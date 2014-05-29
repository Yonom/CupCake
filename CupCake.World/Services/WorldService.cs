using CupCake.Core.Events;
using CupCake.Core.Services;
using CupCake.Messages.Blocks;
using CupCake.Messages.Events.Receive;
using CupCake.World.Blocks;
using CupCake.World.Events;
using PlayerIOClient;
using WorldPortalBlock = CupCake.World.Blocks.WorldPortalBlock;

namespace CupCake.World.Services
{
    public class WorldService : CupCakeService
    {
        private const uint InitOffset = 17;
        private WorldBlock[,,] _blocks;

        private int _sizeX;

        private int _sizeY;

        public int SizeX
        {
            get { return this._sizeX; }
        }

        public int SizeY
        {
            get { return this._sizeY; }
        }

        public WorldBlock this[Layer layer, int x, int y]
        {
            get { return this._blocks[(int)layer, x, y]; }
        }

        private static WorldBlock[,,] ParseWorld(Message m, int sizeX, int sizeY, uint offset)
        {
            uint start = 0;
            for (uint i = offset; i <= m.Count - 1u; i++)
            {
                if (m[i] as string != null && m.GetString(i) == "ws")
                {
                    start = i + 1u;
                    break;
                }
            }

            WorldBlock[,,] worldArray = GetEmptyWorld(sizeX, sizeY, Block.BlockGravityNothing, Block.BlockGravityNothing);

            uint pointer = start;
            do
            {
                if (m[pointer] as string != null && m.GetString(pointer) == "we")
                {
                    break;
                }

                var block1 = (Block)m.GetInteger(pointer);
                var layer = (Layer)m.GetInteger(pointer + 1u);
                byte[] byteArrayX = m.GetByteArray(pointer + 2u);
                byte[] byteArrayY = m.GetByteArray(pointer + 3u);
                pointer += 4u;

                switch (block1)
                {
                    case Block.BlockDoorCoinDoor:
                    case Block.BlockGateCoinGate:
                        int coinsToCollect = m.GetInteger(pointer);
                        pointer += 1u;

                        for (int i = 0; i <= byteArrayX.Length - 1; i += 2)
                        {
                            worldArray[
                                (int)layer, byteArrayX[i] * 256 + byteArrayX[i + 1],
                                byteArrayY[i] * 256 + byteArrayY[i + 1]] = new WorldCoinDoorBlock(
                                    (CoinDoorBlock)block1, coinsToCollect);
                        }


                        break;
                    case Block.BlockMusicPiano:
                    case Block.BlockMusicDrum:
                        int soundId = m.GetInteger(pointer);
                        pointer += 1u;

                        for (int i = 0; i <= byteArrayX.Length - 1; i += 2)
                        {
                            worldArray[
                                (int)layer, byteArrayX[i] * 256 + byteArrayX[i + 1],
                                byteArrayY[i] * 256 + byteArrayY[i + 1]] = new WorldSoundBlock((SoundBlock)block1,
                                    soundId);
                        }


                        break;
                    case Block.BlockHazardSpike:
                    case Block.DecorationSciFi2013BlueSlope:
                    case Block.DecorationSciFi2013BlueStraight:
                    case Block.DecorationSciFi2013YellowSlope:
                    case Block.DecorationSciFi2013YellowStraight:
                    case Block.DecorationSciFi2013GreenSlope:
                    case Block.DecorationSciFi2013GreenStraight:
                        int rotation = m.GetInteger(pointer);
                        pointer += 1u;

                        for (int i = 0; i <= byteArrayX.Length - 1; i += 2)
                        {
                            worldArray[
                                (int)layer, byteArrayX[i] * 256 + byteArrayX[i + 1],
                                byteArrayY[i] * 256 + byteArrayY[i + 1]] =
                                new WorldRotatableBlock((RotatableBlock)block1, rotation);
                        }


                        break;
                    case Block.BlockPortal:
                    case Block.BlockInvisiblePortal:
                        var portalRotation = (PortalRotation)m.GetInteger(pointer);
                        int portalId = m.GetInteger(pointer + 1u);
                        int portalTarget = m.GetInteger(pointer + 2u);
                        pointer += 3u;

                        for (int i = 0; i <= byteArrayX.Length - 1; i += 2)
                        {
                            worldArray[
                                (int)layer, byteArrayX[i] * 256 + byteArrayX[i + 1],
                                byteArrayY[i] * 256 + byteArrayY[i + 1]] = new WorldPortalBlock((PortalBlock)block1,
                                    portalRotation, portalId, portalTarget);
                        }


                        break;
                    case Block.BlockWorldPortal:
                        string worldPortalTarget = m.GetString(pointer);
                        pointer += 1u;

                        for (int i = 0; i <= byteArrayX.Length - 1; i += 2)
                        {
                            worldArray[
                                (int)layer, byteArrayX[i] * 256 + byteArrayX[i + 1],
                                byteArrayY[i] * 256 + byteArrayY[i + 1]] =
                                new WorldWorldPortalBlock((Messages.Blocks.WorldPortalBlock)block1, worldPortalTarget);
                        }


                        break;
                    case Block.DecorationSign:
                    case Block.DecorationLabel:
                        string text = m.GetString(pointer);
                        pointer += 1u;

                        for (int i = 0; i <= byteArrayX.Length - 1; i += 2)
                        {
                            worldArray[
                                (int)layer, byteArrayX[i] * 256 + byteArrayX[i + 1],
                                byteArrayY[i] * 256 + byteArrayY[i + 1]] = new WorldLabelBlock((LabelBlock)block1, text);
                        }


                        break;
                    default:
                        for (int i = 0; i <= byteArrayX.Length - 1; i += 2)
                        {
                            worldArray[
                                (int)layer, byteArrayX[i] * 256 + byteArrayX[i + 1],
                                byteArrayY[i] * 256 + byteArrayY[i + 1]] = new WorldBlock(block1);
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
                for (int x = 1; x <= maxX; x++)
                {
                    for (int y = 1; y <= maxY; y++)
                    {
                        blockArray[(int)l, x, y] = new WorldBlock(fillBlock);
                        //Create a new instance for every block
                    }
                }
            }

            // Border drawing
            maxX -= 1;
            for (int y = maxY; y >= 0; y += -1)
            {
                blockArray[0, 0, y] = new WorldBlock(borderBlock);
                blockArray[0, maxY, y] = new WorldBlock(borderBlock);
            }

            for (int x = 1; x <= maxX; x++)
            {
                blockArray[0, x, 0] = new WorldBlock(borderBlock);
                blockArray[0, x, maxY] = new WorldBlock(borderBlock);
            }

            return blockArray;
        }

        protected override void Enable()
        {
            this.Events.Bind<InitReceiveEvent>(this.OnInit, EventPriority.High);
            this.Events.Bind<BlockPlaceReceiveEvent>(this.OnBlockPlace, EventPriority.Lowest);
            this.Events.Bind<CoinDoorPlaceReceiveEvent>(this.OnCoinDoorPlace, EventPriority.Lowest);
            this.Events.Bind<LabelPlaceReceiveEvent>(this.OnLabelPlace, EventPriority.Lowest);
            this.Events.Bind<PortalPlaceReceiveEvent>(this.OnPortalPlace, EventPriority.Lowest);
            this.Events.Bind<WorldPortalPlaceReceiveEvent>(this.OnWorldPortalPlace, EventPriority.Lowest);
            this.Events.Bind<SoundPlaceReceiveEvent>(this.OnSoundPlace, EventPriority.Lowest);
            this.Events.Bind<RotatablePlaceReceiveEvent>(this.OnRotatablePlace, EventPriority.Lowest);
            this.Events.Bind<ResetReceiveEvent>(this.OnReset, EventPriority.High);
            this.Events.Bind<ClearReceiveEvent>(this.OnClear, EventPriority.High);
        }

        private void OnInit(object sender, InitReceiveEvent e)
        {
            this._sizeX = e.SizeX;
            this._sizeY = e.SizeY;
            this._blocks = ParseWorld(e.PlayerIOMessage, e.SizeX, e.SizeY, InitOffset);
        }

        private void OnBlockPlace(object sender, BlockPlaceReceiveEvent e)
        {
            var block = new WorldBlock(e.Block);
            this._blocks[(int)e.Layer, e.PosX, e.PosY] = block;

            this.Events.Raise(new BlockPlaceEvent(e.PosX, e.PosY, e.Layer, block));
        }

        private void OnCoinDoorPlace(object sender, CoinDoorPlaceReceiveEvent e)
        {
            var block = new WorldCoinDoorBlock(e.CoinDoorBlock, e.CoinsToOpen);
            this._blocks[(int)e.Layer, e.PosX, e.PosY] = block;

            this.Events.Raise(new BlockPlaceEvent(e.PosX, e.PosY, e.Layer, block));
        }

        private void OnLabelPlace(object sender, LabelPlaceReceiveEvent e)
        {
            var block = new WorldLabelBlock(e.LabelBlock, e.Text);
            this._blocks[(int)e.Layer, e.PosX, e.PosY] = block;

            this.Events.Raise(new BlockPlaceEvent(e.PosX, e.PosY, e.Layer, block));
        }

        private void OnPortalPlace(object sender, PortalPlaceReceiveEvent e)
        {
            var block = new WorldPortalBlock(e.PortalBlock, e.PortalRotation, e.PortalId, e.PortalTarget);
            this._blocks[(int)e.Layer, e.PosX, e.PosY] = block;

            this.Events.Raise(new BlockPlaceEvent(e.PosX, e.PosY, e.Layer, block));
        }

        private void OnWorldPortalPlace(object sender, WorldPortalPlaceReceiveEvent e)
        {
            var block = new WorldWorldPortalBlock(e.WorldPortalBlock, e.WorldPortalTarget);
            this._blocks[(int)e.Layer, e.PosX, e.PosY] = block;

            this.Events.Raise(new BlockPlaceEvent(e.PosX, e.PosY, e.Layer, block));
        }

        private void OnSoundPlace(object sender, SoundPlaceReceiveEvent e)
        {
            var block = new WorldSoundBlock(e.SoundBlock, e.SoundId);
            this._blocks[(int)e.Layer, e.PosX, e.PosY] = block;

            this.Events.Raise(new BlockPlaceEvent(e.PosX, e.PosY, e.Layer, block));
        }

        private void OnRotatablePlace(object sender, RotatablePlaceReceiveEvent e)
        {
            var block = new WorldRotatableBlock(e.RotatableBlock, e.Rotation);
            this._blocks[(int)e.Layer, e.PosX, e.PosY] = block;

            this.Events.Raise(new BlockPlaceEvent(e.PosX, e.PosY, e.Layer, block));
        }

        private void OnReset(object sender, ResetReceiveEvent e)
        {
            this._blocks = ParseWorld(e.PlayerIOMessage, this._sizeX, this._sizeY, 0);
        }

        private void OnClear(object sender, ClearReceiveEvent e)
        {
            this._sizeX = e.RoomWidth;
            this._sizeY = e.RoomHeight;
            this._blocks = GetEmptyWorld(this.SizeX, this.SizeY, e.FillBlock, e.BorderBlock);
        }
    }
}