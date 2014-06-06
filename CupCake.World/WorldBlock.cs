using System;
using CupCake.Core.Metadata;
using CupCake.Messages.Blocks;
using CupCake.Messages.Send;

namespace CupCake.World
{
    public class WorldBlock
    {
        private BlockData _data;
        public Layer Layer { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public virtual BlockType BlockType { get; private set; }
        public Block Block { get; private set; }

        private Lazy<MetadataManager> _metadata;
        public MetadataManager Metadata
        {
            get { return this._metadata.Value; }
        }

        public string Text {
            get
            {
                if (this.BlockType != BlockType.Label)
                    throw new InvalidOperationException("This property can only be accessed on Label blocks.");

                return _data.Text;
            }
        }
        public string WorldPortalTarget
        {
            get
            {
                if (this.BlockType != BlockType.WorldPortal)
                    throw new InvalidOperationException("This property can only be accessed on WorldPortal blocks.");

                return _data.WorldPortalTarget;
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
            this._metadata = new Lazy<MetadataManager>(() => new MetadataManager());
        }

        internal WorldBlock(Layer layer, int x, int y, Block block) 
            : this()
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
                WorldPortalTarget = worldId
            };
        }

        public bool IsSame(IBlockPlaceSendEvent other)
        {
            if (other == null)
            {
                return false;
            }

            bool result = this.Block == other.Block;

            if (other is BlockPlaceSendEvent)
            {
                return result;
            }
            var coinEvent = other as CoinDoorPlaceSendEvent;
            if (coinEvent != null)
            {
                if (this._data.CoinsToCollect != coinEvent.CoinsToCollect)
                    result = false;
                return result;
            }
            var labelEvent = other as LabelPlaceSendEvent;
            if (labelEvent != null)
            {
                if (this._data.Text != labelEvent.Text)
                    result = false;
                return result;
            }
            var worldPortalEvent = other as WorldPortalPlaceSendEvent;
            if (worldPortalEvent != null)
            {
                if (this._data.WorldPortalTarget != worldPortalEvent.WorldPortalTarget)
                    result = false;
                return result;
            }
            var rotatableEvent = other as RotatablePlaceSendEvent;
            if (rotatableEvent != null)
            {
                if (this._data.Rotation != rotatableEvent.Rotation)
                    result = false;
                return result;
            }
            var soundEvent = other as SoundPlaceSendEvent;
            if (soundEvent != null)
            {
                if (this._data.SoundId != soundEvent.SoundId)
                    result = false;
                return result;
            }
            var portalEvent = other as PortalPlaceSendEvent;
            if (portalEvent != null)
            {
                if (this._data.PortalId != portalEvent.PortalId ||
                    this._data.PortalTarget != portalEvent.PortalTarget ||
                    this._data.PortalRotation != portalEvent.PortalRotation)
                    result = false;

                return result;
            }

            throw new NotSupportedException("The given send message is not supported.");
        }

        private struct BlockData
        {
            internal string Text { get; set; }
            internal string WorldPortalTarget { get; set; }
            internal int CoinsToCollect { get; set; }
            internal int PortalId { get; set; }
            internal int PortalTarget { get; set; }
            internal PortalRotation PortalRotation { get; set; }
            internal int SoundId { get; set; }
            internal int Rotation { get; set; }
        }
    }
}