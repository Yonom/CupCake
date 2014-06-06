using CupCake.Core;
using CupCake.Core.Events;
using CupCake.Messages.Blocks;
using CupCake.Messages.Receive;
using PlayerIOClient;

namespace CupCake.World
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
                    case Block.DoorCoinDoor:
                    case Block.GateCoinGate:
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
                for (int x = 1; x <= maxX; x++)
                {
                    for (int y = 1; y <= maxY; y++)
                    {
                        blockArray[(int)l, x, y] = new WorldBlock(l, x, y, Block.GravityNothing);
                        //Create a new instance for every block
                    }
                }
            }

            // Border drawing
            maxX -= 1;
            for (int y = maxY; y >= 0; y += -1)
            {
                blockArray[0, 0, y] = new WorldBlock(Layer.Foreground, 0, y, borderBlock);
                blockArray[0, maxY, y] = new WorldBlock(Layer.Foreground, maxY, y, borderBlock);
                blockArray[1, 0, y] = new WorldBlock(Layer.Background, 0, y, Block.GravityNothing);
                blockArray[1, maxY, y] = new WorldBlock(Layer.Background, maxY, y, Block.GravityNothing);
            }

            for (int x = 1; x <= maxX; x++)
            {
                blockArray[0, x, 0] = new WorldBlock(Layer.Foreground, x, 0, borderBlock);
                blockArray[0, x, maxY] = new WorldBlock(Layer.Foreground, x, maxY, borderBlock);
                blockArray[1, x, 0] = new WorldBlock(Layer.Background, x, 0, Block.GravityNothing);
                blockArray[1, x, maxY] = new WorldBlock(Layer.Background, x, maxY, Block.GravityNothing);
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
            WorldBlock b = this._blocks[(int)e.Layer, e.PosX, e.PosY];
            b.SetBlock(e.Block);
            this.Events.Raise(new BlockPlaceEvent(b));
        }

        private void OnCoinDoorPlace(object sender, CoinDoorPlaceReceiveEvent e)
        {
            WorldBlock b = this._blocks[(int)e.Layer, e.PosX, e.PosY];
            b.SetCoinDoor(e.Block, e.CoinsToOpen);

            this.Events.Raise(new BlockPlaceEvent(b));
        }

        private void OnLabelPlace(object sender, LabelPlaceReceiveEvent e)
        {
            WorldBlock b = this._blocks[(int)e.Layer, e.PosX, e.PosY];
            b.SetLabel(e.Block, e.Text);

            this.Events.Raise(new BlockPlaceEvent(b));
        }

        private void OnPortalPlace(object sender, PortalPlaceReceiveEvent e)
        {
            WorldBlock b = this._blocks[(int)e.Layer, e.PosX, e.PosY];
            b.SetPortal(e.Block, e.PortalId, e.PortalTarget, e.PortalRotation);

            this.Events.Raise(new BlockPlaceEvent(b));
        }

        private void OnWorldPortalPlace(object sender, WorldPortalPlaceReceiveEvent e)
        {
            WorldBlock b = this._blocks[(int)e.Layer, e.PosX, e.PosY];
            b.SetWorldPortal(e.Block, e.WorldPortalTarget);

            this.Events.Raise(new BlockPlaceEvent(b));
        }

        private void OnSoundPlace(object sender, SoundPlaceReceiveEvent e)
        {
            WorldBlock b = this._blocks[(int)e.Layer, e.PosX, e.PosY];
            b.SetSound(e.Block, e.SoundId);

            this.Events.Raise(new BlockPlaceEvent(b));
        }

        private void OnRotatablePlace(object sender, RotatablePlaceReceiveEvent e)
        {
            WorldBlock b = this._blocks[(int)e.Layer, e.PosX, e.PosY];
            b.SetRotatable(e.Block, e.Rotation);

            this.Events.Raise(new BlockPlaceEvent(b));
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