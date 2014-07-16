using System;
using System.Diagnostics;
using CupCake.Core.Metadata;
using CupCake.Messages.Blocks;
using CupCake.Messages.Send;

namespace CupCake.World
{
    [DebuggerDisplay("Block = {Block}, Data = {DebuggerData()}")]
    public class WorldBlock
    {
        private readonly Lazy<MetadataStore> _metadata;
        private BlockData _data;

        public WorldBlock()
        {
            _data = new BlockData();
            _metadata = new Lazy<MetadataStore>(() => new MetadataStore());
        }

        internal WorldBlock(Layer layer, int x, int y, Block block)
            : this()
        {
            Layer = layer;
            X = x;
            Y = y;
            SetBlock(block);
        }

        public Layer Layer { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public virtual BlockType BlockType { get; private set; }
        public Block Block { get; private set; }

        public MetadataStore Metadata
        {
            get { return _metadata.Value; }
        }

        public string Text
        {
            get
            {
                if (BlockType != BlockType.Label)
                    throw new InvalidOperationException("This property can only be accessed on Label blocks.");

                return _data.Text;
            }
        }

        public string WorldPortalTarget
        {
            get
            {
                if (BlockType != BlockType.WorldPortal)
                    throw new InvalidOperationException("This property can only be accessed on WorldPortal blocks.");

                return _data.WorldPortalTarget;
            }
        }

        public int CoinsToCollect
        {
            get
            {
                if (BlockType != BlockType.CoinDoor)
                    throw new InvalidOperationException("This property can only be accessed on CoinDoor blocks.");

                return _data.CoinsToCollect;
            }
        }

        public int PortalId
        {
            get
            {
                if (BlockType != BlockType.Portal)
                    throw new InvalidOperationException("This property can only be accessed on Portal blocks.");

                return _data.PortalId;
            }
        }

        public int PortalTarget
        {
            get
            {
                if (BlockType != BlockType.Portal)
                    throw new InvalidOperationException("This property can only be accessed on Portal blocks.");

                return _data.PortalTarget;
            }
        }

        public PortalRotation PortalRotation
        {
            get
            {
                if (BlockType != BlockType.Portal)
                    throw new InvalidOperationException("This property can only be accessed on Portal blocks.");

                return _data.PortalRotation;
            }
        }

        public int SoundId
        {
            get
            {
                if (BlockType != BlockType.Sound)
                    throw new InvalidOperationException("This property can only be accessed on Sound blocks.");

                return _data.SoundId;
            }
        }

        public int Rotation
        {
            get
            {
                if (BlockType != BlockType.Rotatable)
                    throw new InvalidOperationException("This property can only be accessed on Rotatable blocks.");

                return _data.Rotation;
            }
        }

        internal void SetBlock(Block block)
        {
            BlockType = BlockType.Normal;
            Block = block;
            _data = new BlockData();
        }

        internal void SetCoinDoor(CoinDoorBlock block, int coinsToCollect)
        {
            BlockType = BlockType.CoinDoor;
            Block = (Block) block;

            _data = new BlockData
            {
                CoinsToCollect = coinsToCollect
            };
        }

        internal void SetPortal(PortalBlock block, int portalId, int portalTarget, PortalRotation portalRotation)
        {
            BlockType = BlockType.Portal;
            Block = (Block) block;

            _data = new BlockData
            {
                PortalId = portalId,
                PortalTarget = portalTarget,
                PortalRotation = portalRotation
            };
        }

        internal void SetSound(SoundBlock block, int soundId)
        {
            BlockType = BlockType.Sound;
            Block = (Block) block;

            _data = new BlockData
            {
                SoundId = soundId
            };
        }

        internal void SetRotatable(RotatableBlock block, int rotation)
        {
            BlockType = BlockType.Rotatable;
            Block = (Block) block;

            _data = new BlockData
            {
                Rotation = rotation
            };
        }

        internal void SetLabel(LabelBlock block, string text)
        {
            BlockType = BlockType.Label;
            Block = (Block) block;

            _data = new BlockData
            {
                Text = text
            };
        }

        internal void SetWorldPortal(WorldPortalBlock block, string worldId)
        {
            BlockType = BlockType.WorldPortal;
            Block = (Block) block;

            _data = new BlockData
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

            bool result = Block == other.Block;

            if (other is BlockPlaceSendEvent)
            {
                return result;
            }
            var coinEvent = other as CoinDoorPlaceSendEvent;
            if (coinEvent != null)
            {
                if (_data.CoinsToCollect != coinEvent.CoinsToCollect)
                    result = false;
                return result;
            }
            var labelEvent = other as LabelPlaceSendEvent;
            if (labelEvent != null)
            {
                if (_data.Text != labelEvent.Text)
                    result = false;
                return result;
            }
            var worldPortalEvent = other as WorldPortalPlaceSendEvent;
            if (worldPortalEvent != null)
            {
                if (_data.WorldPortalTarget != worldPortalEvent.WorldPortalTarget)
                    result = false;
                return result;
            }
            var rotatableEvent = other as RotatablePlaceSendEvent;
            if (rotatableEvent != null)
            {
                if (_data.Rotation != rotatableEvent.Rotation)
                    result = false;
                return result;
            }
            var soundEvent = other as SoundPlaceSendEvent;
            if (soundEvent != null)
            {
                if (_data.SoundId != soundEvent.SoundId)
                    result = false;
                return result;
            }
            var portalEvent = other as PortalPlaceSendEvent;
            if (portalEvent != null)
            {
                if (_data.PortalId != portalEvent.PortalId ||
                    _data.PortalTarget != portalEvent.PortalTarget ||
                    _data.PortalRotation != portalEvent.PortalRotation)
                    result = false;

                return result;
            }

            throw new NotSupportedException("The given send message is not supported.");
        }

// ReSharper disable once UnusedMember.Local
        private string DebuggerData()
        {
            switch (BlockType)
            {
                case BlockType.Normal:
                    return String.Format("Layer = {0}", Layer);
                case BlockType.CoinDoor:
                    return String.Format("CoinsToCollect = {0}", CoinsToCollect);
                case BlockType.Rotatable:
                    return String.Format("Rotation = {0}", Rotation);
                case BlockType.Sound:
                    return String.Format("SoundId = {0}", SoundId);
                case BlockType.Label:
                    return String.Format("Text = {0}", Text);
                case BlockType.Portal:
                    return String.Format("Id = {0}, Target = {1}, Rotation = {2}", PortalId, PortalTarget,
                        PortalRotation);
                case BlockType.WorldPortal:
                    return String.Format("Target = {0}", WorldPortalTarget);
                default:
                    return String.Empty;
            }
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