using System;
using System.Collections.Generic;
using System.Diagnostics;
using CupCake.Core;
using CupCake.Core.Events;
using CupCake.Core.Log;
using CupCake.Messages.Blocks;
using CupCake.Messages.Receive;
using CupCake.Players;
using PlayerIOClient;

namespace CupCake.World
{
    /// <summary>
    ///     Class WorldService.
    ///     Stores all the blocks inside a room.
    /// </summary>
    [DebuggerDisplay("RoomWidth = {RoomWidth}, RoomHeight = {RoomHeight}")]
    public sealed class WorldService : CupCakeService
    {
        private const uint InitOffset = 17;

        private WorldBlock[,,] _blocks;
        private PlayerService _playerService;

        /// <summary>
        ///     Gets the width of the room.
        /// </summary>
        /// <value>
        ///     The width of the room.
        /// </value>
        public int RoomWidth { get; private set; }

        /// <summary>
        ///     Gets the height of the room.
        /// </summary>
        /// <value>
        ///     The height of the room.
        /// </value>
        public int RoomHeight { get; private set; }

        /// <summary>
        ///     Gets the <see cref="WorldBlock" /> at the specified location.
        /// </summary>
        /// <value>
        ///     The <see cref="WorldBlock" />.
        /// </value>
        /// <param name="layer">The layer.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public WorldBlock this[Layer layer, int x, int y]
        {
            get { return this._blocks[(int)layer, x, y]; }
        }

        private WorldBlock[,,] ParseWorld(Message m, int sizeX, int sizeY, uint offset)
        {
            // Find the start of the world
            uint start = 0;
            for (uint i = offset; i <= m.Count - 1; i++)
            {
                string strValue;
                if ((strValue = m[i] as string) != null && strValue == "ws")
                {
                    start = i + 1;
                    break;
                }
            }

            // Generate an empty world
            WorldBlock[,,] worldArray = this.GetEmptyWorld(sizeX, sizeY);

            // Parse the world data
            uint pointer = start;
            string strValue2;
            while ((strValue2 = m[pointer] as string) == null || strValue2 != "we")
            {
                var block = (Block)m.GetInteger(pointer++);
                int l = m.GetInteger(pointer++);

                try
                {
                    byte[] byteArrayX = m.GetByteArray(pointer++);
                    byte[] byteArrayY = m.GetByteArray(pointer++);
                    IEnumerable<WorldBlock> wblocks = this.GetBlocks(l, byteArrayX, byteArrayY, worldArray);

                    if (BlockUtils.IsCoinDoorOrGate(block))
                    {
                        uint coinsToCollect = m.GetUInt(pointer++);
                        foreach (WorldBlock wblock in wblocks)
                            wblock.SetCoinDoor((CoinDoorBlock)block, coinsToCollect);
                    }
                    else if (BlockUtils.IsPurpleDoorOrGateOrSwitch(block))
                    {
                        uint purpleId = m.GetUInt(pointer++);

                        foreach (WorldBlock wblock in wblocks)
                            wblock.SetPurpleDoor((PurpleDoorBlock)block, purpleId);
                    }
                    else if (BlockUtils.IsDeathDoorOrGate(block))
                    {
                        uint deathsRequired = m.GetUInt(pointer++);
                        foreach (WorldBlock wblock in wblocks)
                            wblock.SetDeathDoor((DeathDoorBlock)block, deathsRequired);
                    }
                    else if (BlockUtils.IsSound(block))
                    {
                        uint soundId = m.GetUInt(pointer++);
                        foreach (WorldBlock wblock in wblocks)
                            wblock.SetSound((SoundBlock)block, soundId);
                    }
                    else if (BlockUtils.IsRotatable(block))
                    {
                        uint rotation = m.GetUInt(pointer++);
                        foreach (WorldBlock wblock in wblocks)
                            wblock.SetRotatable((RotatableBlock)block, rotation);
                    }
                    else if (BlockUtils.IsPortal(block))
                    {
                        var portalRotation = (PortalRotation)m.GetUInt(pointer++);
                        uint portalId = m.GetUInt(pointer++);
                        uint portalTarget = m.GetUInt(pointer++);
                        foreach (WorldBlock wblock in wblocks)
                            wblock.SetPortal((PortalBlock)block, portalId, portalTarget, portalRotation);
                    }
                    else if (BlockUtils.IsWorldPortal(block))
                    {
                        string worldPortalTarget = m.GetString(pointer++);
                        foreach (WorldBlock wblock in wblocks)
                            wblock.SetWorldPortal((WorldPortalBlock)block, worldPortalTarget);
                    }
                    else if (BlockUtils.IsSign(block))
                    {
                        string text = m.GetString(pointer++);
                        foreach (WorldBlock wblock in wblocks)
                            wblock.SetSign(block, text);

                    }
                    else if (BlockUtils.IsLabel(block))
                    {
                        string text = m.GetString(pointer++);
                        string textColor = m.GetString(pointer++);
                        foreach (WorldBlock wblock in wblocks)
                        {
                            wblock.SetLabel(block, text, textColor);
                        }
                    }
                    else
                    {
                        foreach (WorldBlock wblock in wblocks)
                            wblock.SetBlock(block);
                    }
                }
                catch (Exception ex)
                {
                    this.Logger.Log(LogPriority.Error,
                        "Exception when parsing block {0} on layer {1}. The error was: {2}", block, l, ex.Message);
                }
            }

            return worldArray;
        }

        private IEnumerable<WorldBlock> GetBlocks(int l, byte[] byteArrayX, byte[] byteArrayY, WorldBlock[,,] worldArray)
        {
            for (int i = 0; i <= byteArrayX.Length - 1; i += 2)
            {
                int x = byteArrayX[i] * 256 + byteArrayX[i + 1];
                int y = byteArrayY[i] * 256 + byteArrayY[i + 1];

                yield return worldArray[l, x, y];
            }
        }

        private WorldBlock[,,] GetEmptyWorld(int sizeX, int sizeY, Block fillBlock = Block.GravityNothing,
            Block borderBlock = Block.GravityNothing)
        {
            var blockArray = new WorldBlock[2, sizeX, sizeY];

            int maxX = sizeX - 1;
            int maxY = sizeY - 1;

            // Fill the middle with GravityNothing blocks
            for (var l = Layer.Background; l >= Layer.Foreground; l += -1)
                for (int x = 0; x <= maxX; x++)
                    for (int y = 0; y <= maxY; y++)
                        this.NewBlock(blockArray, l, x, y, fillBlock);

            // Border drawing
            for (int y = 0; y <= maxY; y++)
            {
                blockArray[0, 0, y].SetBlock(borderBlock);
                blockArray[0, maxX, y].SetBlock(borderBlock);
            }

            for (int x = 0; x <= maxX; x++)
            {
                blockArray[0, x, 0].SetBlock(borderBlock);
                blockArray[0, x, maxY].SetBlock(borderBlock);
            }

            return blockArray;
        }

        private void NewBlock(WorldBlock[,,] blockArray, Layer layer, int x, int y, Block block)
        {
            blockArray[(int)layer, x, y] = new WorldBlock(this.MetadataPlatform, layer, x, y, block);
        }

        /// <summary>
        ///     Enables this instance.
        /// </summary>
        protected override void Enable()
        {
            this.Events.Bind<InitReceiveEvent>(this.OnInit, EventPriority.High);
            this.Events.Bind<BlockPlaceReceiveEvent>(this.OnBlockPlace, EventPriority.High);
            this.Events.Bind<CoinDoorPlaceReceiveEvent>(this.OnCoinDoorPlace, EventPriority.High);
            this.Events.Bind<DeathDoorPlaceReceiveEvent>(this.OnDeathDoorPlace, EventPriority.High);
            this.Events.Bind<PurpleDoorPlaceReceiveEvent>(this.OnPurpleDoorPlace, EventPriority.High);
            this.Events.Bind<SignPlaceReceiveEvent>(this.OnSignPlace, EventPriority.High);
            this.Events.Bind<LabelPlaceReceiveEvent>(this.OnLabelPlace, EventPriority.High);
            this.Events.Bind<PortalPlaceReceiveEvent>(this.OnPortalPlace, EventPriority.High);
            this.Events.Bind<WorldPortalPlaceReceiveEvent>(this.OnWorldPortalPlace, EventPriority.High);
            this.Events.Bind<SoundPlaceReceiveEvent>(this.OnSoundPlace, EventPriority.High);
            this.Events.Bind<RotatablePlaceReceiveEvent>(this.OnRotatablePlace, EventPriority.High);
            this.Events.Bind<ResetReceiveEvent>(this.OnReset, EventPriority.High);
            this.Events.Bind<ClearReceiveEvent>(this.OnClear, EventPriority.High);

            this.ServiceLoader.EnableComplete += this.ServiceLoader_EnableComplete;
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.ServiceLoader.EnableComplete -= this.ServiceLoader_EnableComplete;

                this._blocks = null;
                this._playerService = null;
                this.RoomHeight = 0;
                this.RoomWidth = 0;
            }

            base.Dispose(disposing);
        }

        private void ServiceLoader_EnableComplete(object sender, EventArgs e)
        {
            this._playerService = this.ServiceLoader.Get<PlayerService>();
        }

        private void OnInit(object sender, InitReceiveEvent e)
        {
            this.RoomWidth = e.RoomWidth;
            this.RoomHeight = e.RoomHeight;
            this.Events.Raise(new ResizeWorldEvent(e.RoomWidth, e.RoomHeight));
            this._blocks = this.ParseWorld(e.PlayerIOMessage, e.RoomWidth, e.RoomHeight, InitOffset);
        }

        private void OnBlockPlace(object sender, BlockPlaceReceiveEvent e)
        {
            WorldBlock b = this._blocks[(int)e.Layer, e.PosX, e.PosY];
            WorldBlock oldBlock = b.Clone();
            b.SetBlock(e.Block);

            this.RaisePlaceWorld(b, oldBlock, e.UserId);
        }

        private void OnCoinDoorPlace(object sender, CoinDoorPlaceReceiveEvent e)
        {
            WorldBlock b = this._blocks[(int)e.Layer, e.PosX, e.PosY];
            WorldBlock oldBlock = b.Clone();
            b.SetCoinDoor(e.Block, e.CoinsToOpen);

            this.RaisePlaceWorld(b, oldBlock);
        }

        private void OnDeathDoorPlace(object sender, DeathDoorPlaceReceiveEvent e)
        {
            WorldBlock b = this._blocks[(int)e.Layer, e.PosX, e.PosY];
            WorldBlock oldBlock = b.Clone();
            b.SetDeathDoor(e.Block, e.DeathsRequired);

            this.RaisePlaceWorld(b, oldBlock);
        }

        private void OnPurpleDoorPlace(object sender, PurpleDoorPlaceReceiveEvent e)
        {
            WorldBlock b = this._blocks[(int)e.Layer, e.PosX, e.PosY];
            WorldBlock oldBlock = b.Clone();
            b.SetPurpleDoor(e.Block, e.PurpleId);

            this.RaisePlaceWorld(b, oldBlock);
        }

        private void OnSignPlace(object sender, SignPlaceReceiveEvent e)
        {
            WorldBlock b = this._blocks[(int)e.Layer, e.PosX, e.PosY];
            WorldBlock oldBlock = b.Clone();
            b.SetSign(e.Block, e.Text);

            this.RaisePlaceWorld(b, oldBlock);
        }

        private void OnLabelPlace(object sender, LabelPlaceReceiveEvent e)
        {
            WorldBlock b = this._blocks[(int)e.Layer, e.PosX, e.PosY];
            WorldBlock oldBlock = b.Clone();
            b.SetLabel(e.Block, e.Text, e.TextColor);

            this.RaisePlaceWorld(b, oldBlock);
        }

        private void OnPortalPlace(object sender, PortalPlaceReceiveEvent e)
        {
            WorldBlock b = this._blocks[(int)e.Layer, e.PosX, e.PosY];
            WorldBlock oldBlock = b.Clone();
            b.SetPortal(e.Block, e.PortalId, e.PortalTarget, e.PortalRotation);

            this.RaisePlaceWorld(b, oldBlock);
        }

        private void OnWorldPortalPlace(object sender, WorldPortalPlaceReceiveEvent e)
        {
            WorldBlock b = this._blocks[(int)e.Layer, e.PosX, e.PosY];
            WorldBlock oldBlock = b.Clone();
            b.SetWorldPortal(e.Block, e.WorldPortalTarget);

            this.RaisePlaceWorld(b, oldBlock);
        }

        private void OnSoundPlace(object sender, SoundPlaceReceiveEvent e)
        {
            WorldBlock b = this._blocks[(int)e.Layer, e.PosX, e.PosY];
            WorldBlock oldBlock = b.Clone();
            b.SetSound(e.Block, e.SoundId);

            this.RaisePlaceWorld(b, oldBlock);
        }

        private void OnRotatablePlace(object sender, RotatablePlaceReceiveEvent e)
        {
            WorldBlock b = this._blocks[(int)e.Layer, e.PosX, e.PosY];
            WorldBlock oldBlock = b.Clone();
            b.SetRotatable(e.Block, e.Rotation);

            this.RaisePlaceWorld(b, oldBlock);
        }

        private void RaisePlaceWorld(WorldBlock b, WorldBlock oldB, int userId = -1)
        {
            Player p;
            this._playerService.TryGetPlayer(userId, out p);

            this.SynchronizePlatform.Do(() =>
                this.Events.Raise(new PlaceWorldEvent(b, oldB, p)));
        }

        private void OnReset(object sender, ResetReceiveEvent e)
        {
            this._blocks = this.ParseWorld(e.PlayerIOMessage, this.RoomWidth, this.RoomHeight, 0);
        }

        private void OnClear(object sender, ClearReceiveEvent e)
        {
            this.RoomWidth = e.RoomWidth;
            this.RoomHeight = e.RoomHeight;
            this.Events.Raise(new ResizeWorldEvent(e.RoomHeight, e.RoomWidth));

            this._blocks = this.GetEmptyWorld(this.RoomWidth, this.RoomHeight, e.FillBlock, e.BorderBlock);
        }
    }
}