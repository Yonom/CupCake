using System;
using System.Diagnostics;
using CupCake.Core;
using CupCake.Core.Events;
using CupCake.Messages.Blocks;
using CupCake.Messages.Receive;
using CupCake.Players;
using PlayerIOClient;

namespace CupCake.World
{
    [DebuggerDisplay("RoomWidth = {RoomWidth}, RoomHeight = {RoomHeight}")]
    public sealed class WorldService : CupCakeService
    {
        private const uint InitOffset = 17;

        private WorldBlock[,,] _blocks;
        private PlayerService _playerService;

        public int RoomWidth { get; private set; }
        public int RoomHeight { get; private set; }

        public WorldBlock this[Layer layer, int x, int y]
        {
            get { return this._blocks[(int)layer, x, y]; }
        }

        private static WorldBlock[,,] ParseWorld(Message m, int sizeX, int sizeY, uint offset)
        {
            // Find the start of the world
            uint start = 0;
            for (uint i = offset; i <= m.Count - 1; i++)
            {
                if (m[i] as string != null && m.GetString(i) == "ws")
                {
                    start = i + 1;
                    break;
                }
            }

            // Generate an empty world
            WorldBlock[,,] worldArray = GetEmptyWorld(sizeX, sizeY, Block.GravityNothing, Block.GravityNothing);

            // Parse the world data
            uint pointer = start;
            do
            {
                // Exit once we reached the end
                if (m[pointer] as string != null && m.GetString(pointer) == "we")
                    break;

                var block = (Block)m.GetInteger(pointer);
                int l = m.GetInteger(pointer + 1);
                byte[] byteArrayX = m.GetByteArray(pointer + 2);
                byte[] byteArrayY = m.GetByteArray(pointer + 3);
                pointer += 4;

                switch (block)
                {
                    case Block.CoinDoor:
                    case Block.CoinGate:
                        int coinsToCollect = m.GetInteger(pointer);
                        pointer += 1;

                        for (int i = 0; i <= byteArrayX.Length - 1; i += 2)
                        {
                            int x = byteArrayX[i] * 256 + byteArrayX[i + 1];
                            int y = byteArrayY[i] * 256 + byteArrayY[i + 1];
                            worldArray[l, x, y].SetCoinDoor((CoinDoorBlock)block, coinsToCollect);
                        }


                        break;
                    case Block.MusicPiano:
                    case Block.MusicDrum:
                        int soundId = m.GetInteger(pointer);
                        pointer += 1;

                        for (int i = 0; i <= byteArrayX.Length - 1; i += 2)
                        {
                            int x = byteArrayX[i] * 256 + byteArrayX[i + 1];
                            int y = byteArrayY[i] * 256 + byteArrayY[i + 1];
                            worldArray[l, x, y].SetSound((SoundBlock)block, soundId);
                        }


                        break;
                    case Block.HazardSpike:
                    case Block.DecorSciFi2013BlueSlope:
                    case Block.DecorSciFi2013BlueStraight:
                    case Block.DecorSciFi2013YellowSlope:
                    case Block.DecorSciFi2013YellowStraight:
                    case Block.DecorSciFi2013GreenSlope:
                    case Block.DecorSciFi2013GreenStraight:
                        int rotation = m.GetInteger(pointer);
                        pointer += 1;

                        for (int i = 0; i <= byteArrayX.Length - 1; i += 2)
                        {
                            int x = byteArrayX[i] * 256 + byteArrayX[i + 1];
                            int y = byteArrayY[i] * 256 + byteArrayY[i + 1];
                            worldArray[l, x, y].SetRotatable((RotatableBlock)block, rotation);
                        }


                        break;
                    case Block.Portal:
                    case Block.InvisiblePortal:
                        var portalRotation = (PortalRotation)m.GetInteger(pointer);
                        int portalId = m.GetInteger(pointer + 1u);
                        int portalTarget = m.GetInteger(pointer + 2u);
                        pointer += 3;

                        for (int i = 0; i <= byteArrayX.Length - 1; i += 2)
                        {
                            int x = byteArrayX[i] * 256 + byteArrayX[i + 1];
                            int y = byteArrayY[i] * 256 + byteArrayY[i + 1];
                            worldArray[l, x, y].SetPortal((PortalBlock)block, portalId, portalTarget, portalRotation);
                        }


                        break;
                    case Block.WorldPortal:
                        string worldPortalTarget = m.GetString(pointer);
                        pointer += 1;

                        for (int i = 0; i <= byteArrayX.Length - 1; i += 2)
                        {
                            int x = byteArrayX[i] * 256 + byteArrayX[i + 1];
                            int y = byteArrayY[i] * 256 + byteArrayY[i + 1];
                            worldArray[l, x, y].SetWorldPortal((WorldPortalBlock)block, worldPortalTarget);
                        }


                        break;
                    case Block.DecorSign:
                    case Block.DecorLabel:
                        string text = m.GetString(pointer);
                        pointer += 1;

                        for (int i = 0; i <= byteArrayX.Length - 1; i += 2)
                        {
                            int x = byteArrayX[i] * 256 + byteArrayX[i + 1];
                            int y = byteArrayY[i] * 256 + byteArrayY[i + 1];
                            worldArray[l, x, y].SetLabel((LabelBlock)block, text);
                        }


                        break;
                    default:
                        for (int i = 0; i <= byteArrayX.Length - 1; i += 2)
                        {
                            int x = byteArrayX[i] * 256 + byteArrayX[i + 1];
                            int y = byteArrayY[i] * 256 + byteArrayY[i + 1];
                            worldArray[l, x, y].SetBlock(block);
                        }

                        break;
                }
            } while (true);

            return worldArray;
        }

        private static WorldBlock[,,] GetEmptyWorld(int sizeX, int sizeY, Block fillBlock, Block borderBlock)
        {
            var blockArray = new WorldBlock[2, sizeX, sizeY];

            int maxX = sizeX - 1;
            int maxY = sizeY - 1;

            // Fill the middle with GravityNothing blocks
            for (var l = Layer.Background; l >= Layer.Foreground; l += -1)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    for (int y = 0; y <= maxY; y++)
                    {
                        NewBlock(blockArray, l, x, y, fillBlock);
                    }
                }
            }

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

        private static void NewBlock(WorldBlock[,,] blockArray, Layer layer, int x, int y, Block block)
        {
            blockArray[(int)layer, x, y] = new WorldBlock(layer, x, y, block);
        }

        protected override void Enable()
        {
            this.Events.Bind<InitReceiveEvent>(this.OnInit, EventPriority.High);
            this.Events.Bind<BlockPlaceReceiveEvent>(this.OnBlockPlace, EventPriority.High);
            this.Events.Bind<CoinDoorPlaceReceiveEvent>(this.OnCoinDoorPlace, EventPriority.High);
            this.Events.Bind<LabelPlaceReceiveEvent>(this.OnLabelPlace, EventPriority.High);
            this.Events.Bind<PortalPlaceReceiveEvent>(this.OnPortalPlace, EventPriority.High);
            this.Events.Bind<WorldPortalPlaceReceiveEvent>(this.OnWorldPortalPlace, EventPriority.High);
            this.Events.Bind<SoundPlaceReceiveEvent>(this.OnSoundPlace, EventPriority.High);
            this.Events.Bind<RotatablePlaceReceiveEvent>(this.OnRotatablePlace, EventPriority.High);
            this.Events.Bind<ResetReceiveEvent>(this.OnReset, EventPriority.High);
            this.Events.Bind<ClearReceiveEvent>(this.OnClear, EventPriority.High);

            this.ServiceLoader.EnableComplete += this.ServiceLoader_EnableComplete;
        }

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
            this._blocks = ParseWorld(e.PlayerIOMessage, e.RoomWidth, e.RoomHeight, InitOffset);
        }

        private void OnBlockPlace(object sender, BlockPlaceReceiveEvent e)
        {
            WorldBlock b = this._blocks[(int)e.Layer, e.PosX, e.PosY];
            b.SetBlock(e.Block);

            this.RaisePlaceWorld(b, e.UserId);
        }

        private void OnCoinDoorPlace(object sender, CoinDoorPlaceReceiveEvent e)
        {
            WorldBlock b = this._blocks[(int)e.Layer, e.PosX, e.PosY];
            b.SetCoinDoor(e.Block, e.CoinsToOpen);

            this.RaisePlaceWorld(b);
        }

        private void OnLabelPlace(object sender, LabelPlaceReceiveEvent e)
        {
            WorldBlock b = this._blocks[(int)e.Layer, e.PosX, e.PosY];
            b.SetLabel(e.Block, e.Text);

            this.RaisePlaceWorld(b);
        }

        private void OnPortalPlace(object sender, PortalPlaceReceiveEvent e)
        {
            WorldBlock b = this._blocks[(int)e.Layer, e.PosX, e.PosY];
            b.SetPortal(e.Block, e.PortalId, e.PortalTarget, e.PortalRotation);

            this.RaisePlaceWorld(b);
        }

        private void OnWorldPortalPlace(object sender, WorldPortalPlaceReceiveEvent e)
        {
            WorldBlock b = this._blocks[(int)e.Layer, e.PosX, e.PosY];
            b.SetWorldPortal(e.Block, e.WorldPortalTarget);

            this.RaisePlaceWorld(b);
        }

        private void OnSoundPlace(object sender, SoundPlaceReceiveEvent e)
        {
            WorldBlock b = this._blocks[(int)e.Layer, e.PosX, e.PosY];
            b.SetSound(e.Block, e.SoundId);

            this.RaisePlaceWorld(b);
        }

        private void OnRotatablePlace(object sender, RotatablePlaceReceiveEvent e)
        {
            WorldBlock b = this._blocks[(int)e.Layer, e.PosX, e.PosY];
            b.SetRotatable(e.Block, e.Rotation);

            this.RaisePlaceWorld(b, -1);
        }

        private void RaisePlaceWorld(WorldBlock b, int userId = -1)
        {
            Player p;
            this._playerService.TryGetPlayer(userId, out p);

            this.SynchronizePlatform.Do(() =>
                this.Events.Raise(new PlaceWorldEvent(b, p)));
        }

        private void OnReset(object sender, ResetReceiveEvent e)
        {
            this._blocks = ParseWorld(e.PlayerIOMessage, this.RoomWidth, this.RoomHeight, 0);
        }

        private void OnClear(object sender, ClearReceiveEvent e)
        {
            this.RoomWidth = e.RoomWidth;
            this.RoomHeight = e.RoomHeight;
            this.Events.Raise(new ResizeWorldEvent(e.RoomHeight, e.RoomWidth));

            this._blocks = GetEmptyWorld(this.RoomWidth, this.RoomHeight, e.FillBlock, e.BorderBlock);
        }
    }
}