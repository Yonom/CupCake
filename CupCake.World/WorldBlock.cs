using System;
using CupCake.Core.Metadata;
using CupCake.Messages.Blocks;
using CupCake.Messages.Send;

namespace CupCake.World
{
    public class WorldBlock
    {
        private readonly Block _block;
        private BlockData _data;
        public Layer Layer { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public virtual BlockType BlockType { get; private set; }
        public Block Block { get; private set; }

        public string Text {
            get
            {
                if (this.BlockType != BlockType.Label)
                    throw new InvalidOperationException("This property can only be accessed on Label blocks.");

                return _data.Text;
            }
        }
        public string WorldPortalWorldId
        {
            get
            {
                if (this.BlockType != BlockType.WorldPortal)
                    throw new InvalidOperationException("This property can only be accessed on WorldPortal blocks.");

                return _data.WorldPortalWorldId;
            }
        }
        public int CoinsToCollect
        {
            get
            {
                if (this.BlockType != BlockType.CoinDoor)
                    throw new InvalidOperationException("This property can only be accessed on CoinDoor blocks.");

                return _data.CoinsToCollect;
            }
        }
        public int PortalId {             
            get
            {
                if (this.BlockType != BlockType.Portal)
                    throw new InvalidOperationException("This property can only be accessed on Portal blocks.");

                return _data.PortalId;
            }
        }
        public int PortalTarget
        {
            get
            {
                if (this.BlockType != BlockType.Portal)
                    throw new InvalidOperationException("This property can only be accessed on Portal blocks.");

                return _data.PortalTarget;
            }
        }
        public PortalRotation PortalRotation
        {
            get
            {
                if (this.BlockType != BlockType.Portal)
                    throw new InvalidOperationException("This property can only be accessed on Portal blocks.");

                return _data.PortalRotation;
            }
        }
        public int SoundId
        {
            get
            {
                if (this.BlockType != BlockType.Sound)
                    throw new InvalidOperationException("This property can only be accessed on Sound blocks.");

                return _data.SoundId;
            }
        }
        public int Rotation
        {
            get
            {
                if (this.BlockType != BlockType.Rotatable)
                    throw new InvalidOperationException("This property can only be accessed on Rotatable blocks.");

                return _data.Rotation;
            }
        }

        public WorldBlock()
        {
            this._data = new BlockData();
        }

        internal WorldBlock(Layer layer, int x, int y, Block block)
        {
            this.Layer = layer;
            this.X = x;
            this.Y = y;
            this.SetBlock(block);
        }

        internal void SetBlock(Block block)
        {
            this.BlockType = BlockType.Normal;
            this.Block = block;
            this._data = new BlockData();
        }

        internal void SetCoinDoor(CoinDoorBlock block, int coinsToCollect)
        {
            this.BlockType = BlockType.CoinDoor;
            this.Block = (Block)block;

            this._data = new BlockData
            {
                CoinsToCollect = coinsToCollect
            };
        }

        internal void SetPortal(PortalBlock block, int portalId, int portalTarget, PortalRotation portalRotation)
        {
            this.BlockType = BlockType.Portal;
            this.Block = (Block)block;

            this._data = new BlockData
            {
                PortalId = portalId,
                PortalTarget = portalTarget,
                PortalRotation = portalRotation
            };
        }

        internal void SetSound(SoundBlock block, int soundId)
        {
            this.BlockType = BlockType.Sound;
            this.Block = (Block)block;

            this._data = new BlockData
            {
                SoundId = soundId
            };
        }

        internal void SetRotatable(RotatableBlock block, int rotation)
        {
            this.BlockType = BlockType.Rotatable;
            this.Block = (Block)block;

            this._data = new BlockData
            {
                Rotation = rotation
            };
        }

        internal void SetLabel(LabelBlock block, string text)
        {
            this.BlockType = BlockType.Label;
            this.Block = (Block)block;

            this._data = new BlockData
            {
                Text = text
            };
        }

        internal void SetWorldPortal(WorldPortalBlock block, string worldId)
        {
            this.BlockType = BlockType.WorldPortal;
            this.Block = (Block)block;

            this._data = new BlockData
            {
                WorldPortalWorldId = worldId
            };
        }


        public static bool operator ==(WorldBlock a, IBlockPlaceSendEvent b)
        {
            if (((object)a == null) || (b == null))
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(WorldBlock a, IBlockPlaceSendEvent b)
        {
            return !(a == b);
        }

        protected virtual bool Equals(IBlockPlaceSendEvent other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Block == other.Block;
        }

        private struct BlockData
        {
            internal string Text { get; set; }
            internal string WorldPortalWorldId { get; set; }
            internal int CoinsToCollect { get; set; }
            internal int PortalId { get; set; }
            internal int PortalTarget { get; set; }
            internal PortalRotation PortalRotation { get; set; }
            internal int SoundId { get; set; }
            internal int Rotation { get; set; }
        }
    }
}