using System;
using System.Diagnostics;
using CupCake.Core.Metadata;
using CupCake.Messages;
using CupCake.Messages.Blocks;
using CupCake.Messages.Send;

namespace CupCake.World
{
    /// <summary>
    ///     Represents a block in the world.
    /// </summary>
    [DebuggerDisplay("Block = {Block}, Data = {DebuggerData()}")]
    public class WorldBlock : MetadataProvider
    {
        private BlockData _data;

        internal WorldBlock(MetadataPlatform metadataPlatform, Layer layer, int x, int y, Block block)
            : base(metadataPlatform)
        {
            this.Layer = layer;
            this.X = x;
            this.Y = y;
            this.SetBlock(block);
        }

        /// <summary>
        ///     Gets the metadata key.
        /// </summary>
        /// <value>
        ///     The metadata key.
        /// </value>
        protected override object MetadataKey
        {
            get { return new Point3D(this.Layer, this.X, this.Y); }
        }

        /// <summary>
        ///     Gets the layer.
        /// </summary>
        /// <value>
        ///     The layer.
        /// </value>
        public Layer Layer { get; private set; }

        /// <summary>
        ///     Gets the x coordinate of this block.
        /// </summary>
        /// <value>
        ///     The x coordinate of this block.
        /// </value>
        public int X { get; private set; }

        /// <summary>
        ///     Gets the y coordinate of this block.
        /// </summary>
        /// <value>
        ///     The y coordinate of this block.
        /// </value>
        public int Y { get; private set; }

        /// <summary>
        ///     Gets the type of the block.
        /// </summary>
        /// <value>
        ///     The type of the block.
        /// </value>
        public virtual BlockType BlockType { get; private set; }

        /// <summary>
        ///     Gets the block.
        /// </summary>
        /// <value>
        ///     The block.
        /// </value>
        public Block Block { get; private set; }

        /// <summary>
        ///     Gets the metadata.
        /// </summary>
        /// <value>
        ///     The metadata.
        /// </value>
        [Obsolete("Use the Get<T> and Set<T> methods instead.")]
        public MetadataStore Metadata
        {
            get { return this.MetadataStore; }
        }

        /// <summary>
        ///     Gets the Text. (Only on sign or label blocks)
        /// </summary>
        /// <value>
        ///     The text.
        /// </value>
        /// <exception cref="System.InvalidOperationException">This property can only be accessed on Sign or Label blocks.</exception>
        public string Text
        {
            get
            {
                if (this.BlockType != BlockType.Sign && this.BlockType != BlockType.Label)
                    throw new InvalidOperationException("This property can only be accessed on Sign or Label blocks.");

                return this._data.Text;
            }
        }


        /// <summary>
        ///     Gets the TextColor. (Only on label blocks)
        /// </summary>
        /// <value>
        ///     The text color.
        /// </value>
        /// <exception cref="System.InvalidOperationException">This property can only be accessed on Label blocks.</exception>
        public string TextColor
        {
            get
            {
                if (this.BlockType != BlockType.Label)
                    throw new InvalidOperationException("This property can only be accessed on Label blocks.");

                return this._data.TextColor;
            }
        }

        /// <summary>
        ///     Gets the world portal target. (Only on world portal blocks)
        /// </summary>
        /// <value>
        ///     The world portal target.
        /// </value>
        /// <exception cref="System.InvalidOperationException">This property can only be accessed on WorldPortal blocks.</exception>
        public string WorldPortalTarget
        {
            get
            {
                if (this.BlockType != BlockType.WorldPortal)
                    throw new InvalidOperationException("This property can only be accessed on WorldPortal blocks.");

                return this._data.WorldPortalTarget;
            }
        }

        /// <summary>
        ///     Gets the coins to collect. (Only on coin doors)
        /// </summary>
        /// <value>
        ///     The coins to collect.
        /// </value>
        /// <exception cref="System.InvalidOperationException">This property can only be accessed on CoinDoor blocks.</exception>
        public uint CoinsToCollect
        {
            get
            {
                if (this.BlockType != BlockType.CoinDoor)
                    throw new InvalidOperationException("This property can only be accessed on CoinDoor blocks.");

                return this._data.CoinsToCollect;
            }
        }

        /// <summary>
        ///     Gets the purple id.
        /// </summary>
        /// <value>
        ///     The coins to collect.
        /// </value>
        /// <exception cref="System.InvalidOperationException">This property can only be accessed on Purple Door or Gate blocks.</exception>
        public uint PurpleId
        {
            get
            {
                if (this.BlockType != BlockType.Purple)
                    throw new InvalidOperationException("This property can only be accessed on CoinDoor blocks.");

                return this._data.PurpleId;
            }
        }

        /// <summary>
        ///     Gets the deaths required.
        /// </summary>
        /// <value>
        ///     The amount of deaths required.
        /// </value>
        /// <exception cref="System.InvalidOperationException">This property can only be accessed on Death Door or Gate blocks.</exception>
        public uint DeathsRequired
        {
            get
            {
                if (this.BlockType != BlockType.Death)
                    throw new InvalidOperationException("This property can only be accessed on Death blocks.");

                return this._data.DeathsRequired;
            }
        }

        /// <summary>
        ///     Gets the portal identifier.  (Only on portal blocks)
        /// </summary>
        /// <value>
        ///     The portal identifier.
        /// </value>
        /// <exception cref="System.InvalidOperationException">This property can only be accessed on Portal blocks.</exception>
        public uint PortalId
        {
            get
            {
                if (this.BlockType != BlockType.Portal)
                    throw new InvalidOperationException("This property can only be accessed on Portal blocks.");

                return this._data.PortalId;
            }
        }

        /// <summary>
        ///     Gets the portal target.  (Only on portal blocks)
        /// </summary>
        /// <value>
        ///     The portal target.
        /// </value>
        /// <exception cref="System.InvalidOperationException">This property can only be accessed on Portal blocks.</exception>
        public uint PortalTarget
        {
            get
            {
                if (this.BlockType != BlockType.Portal)
                    throw new InvalidOperationException("This property can only be accessed on Portal blocks.");

                return this._data.PortalTarget;
            }
        }

        /// <summary>
        ///     Gets the portal rotation. (Only on portal blocks)
        /// </summary>
        /// <value>
        ///     The portal rotation.
        /// </value>
        /// <exception cref="System.InvalidOperationException">This property can only be accessed on Portal blocks.</exception>
        public PortalRotation PortalRotation
        {
            get
            {
                if (this.BlockType != BlockType.Portal)
                    throw new InvalidOperationException("This property can only be accessed on Portal blocks.");

                return this._data.PortalRotation;
            }
        }

        /// <summary>
        ///     Gets the sound identifier. (Only on sound blocks)
        /// </summary>
        /// <value>
        ///     The sound identifier.
        /// </value>
        /// <exception cref="System.InvalidOperationException">This property can only be accessed on Sound blocks.</exception>
        public uint SoundId
        {
            get
            {
                if (this.BlockType != BlockType.Sound)
                    throw new InvalidOperationException("This property can only be accessed on Sound blocks.");

                return this._data.SoundId;
            }
        }

        /// <summary>
        ///     Gets the piano identifier. (Only on piano blocks)
        /// </summary>
        /// <value>
        ///     The piano identifier.
        /// </value>
        /// <exception cref="System.InvalidOperationException">This property can only be accessed on Piano blocks.</exception>
        public PianoId PianoId
        {
            get
            {
                if (!BlockUtils.IsPiano(this.Block))
                    throw new InvalidOperationException("This property can only be accessed on Piano blocks.");

                return (PianoId)this.SoundId;
            }
        }

        /// <summary>
        ///     Gets the percussion identifier. (Only on drum blocks)
        /// </summary>
        /// <value>
        ///     The percussion identifier.
        /// </value>
        /// <exception cref="System.InvalidOperationException">This property can only be accessed on Drum blocks.</exception>
        public PercussionId PercussionId
        {
            get
            {
                if (!BlockUtils.IsDrum(this.Block))
                    throw new InvalidOperationException("This property can only be accessed on Drum blocks.");

                return (PercussionId)this.SoundId;
            }
        }

        /// <summary>
        ///     Gets the rotation. (Only on rotatable blocks)
        /// </summary>
        /// <value>
        ///     The rotation.
        /// </value>
        /// <exception cref="System.InvalidOperationException">This property can only be accessed on Rotatable blocks.</exception>
        public uint Rotation
        {
            get
            {
                if (this.BlockType != BlockType.Rotatable)
                    throw new InvalidOperationException("This property can only be accessed on Rotatable blocks.");

                return this._data.Rotation;
            }
        }

        /// <summary>
        ///     Gets the spike rotation. (Only on spike blocks)
        /// </summary>
        /// <value>
        ///     The spike rotation.
        /// </value>
        /// <exception cref="System.InvalidOperationException">This property can only be accessed on Spike blocks.</exception>
        public SpikeRotation SpikeRotation
        {
            get
            {
                if (!BlockUtils.IsSpike(this.Block))
                    throw new InvalidOperationException("This property can only be accessed on Spike blocks.");

                return (SpikeRotation)this.Rotation;
            }
        }

        /// <summary>
        ///     Gets the scifi straight rotation. (Only on scifi straight blocks)
        /// </summary>
        /// <value>
        ///     The scifi straight rotation.
        /// </value>
        /// <exception cref="System.InvalidOperationException">This property can only be accessed on SciFiStraight blocks.</exception>
        public SciFiStraightRotation SciFiStraightRotation
        {
            get
            {
                if (!BlockUtils.IsSciFiStraight(this.Block))
                    throw new InvalidOperationException("This property can only be accessed on SciFiStraight blocks.");

                return (SciFiStraightRotation)this.Rotation;
            }
        }


        /// <summary>
        ///     Gets the scifi slope rotation. (Only on scifi slope blocks)
        /// </summary>
        /// <value>
        ///     The scifi slope rotation.
        /// </value>
        /// <exception cref="System.InvalidOperationException">This property can only be accessed on SciFiSlope blocks.</exception>
        public SciFiSlopeRotation SciFiSlopeRotation
        {
            get
            {
                if (!BlockUtils.IsSciFiSlope(this.Block))
                    throw new InvalidOperationException("This property can only be accessed on SciFiSlope blocks.");

                return (SciFiSlopeRotation)this.Rotation;
            }
        }

        internal void SetBlock(Block block)
        {
            this.BlockType = BlockType.Normal;
            this.Block = block;
            this._data = new BlockData();
        }

        internal void SetCoinDoor(CoinDoorBlock block, uint coinsToCollect)
        {
            this.BlockType = BlockType.CoinDoor;
            this.Block = (Block)block;

            this._data = new BlockData
            {
                CoinsToCollect = coinsToCollect
            };
        }

        internal void SetPurpleDoor(PurpleDoorBlock block, uint purpleId)
        {
            this.BlockType = BlockType.Purple;
            this.Block = (Block)block;

            this._data = new BlockData
            {
                PurpleId = purpleId
            };
        }

        internal void SetDeathDoor(DeathDoorBlock block, uint deathsRequired)
        {
            this.BlockType = BlockType.Purple;
            this.Block = (Block)block;

            this._data = new BlockData
            {
                DeathsRequired = deathsRequired
            };
        }

        internal void SetPortal(PortalBlock block, uint portalId, uint portalTarget, PortalRotation portalRotation)
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

        internal void SetSound(SoundBlock block, uint soundId)
        {
            this.BlockType = BlockType.Sound;
            this.Block = (Block)block;

            this._data = new BlockData
            {
                SoundId = soundId
            };
        }

        internal void SetRotatable(RotatableBlock block, uint rotation)
        {
            this.BlockType = BlockType.Rotatable;
            this.Block = (Block)block;

            this._data = new BlockData
            {
                Rotation = rotation
            };
        }

        internal void SetSign(SignBlock block, string text)
        {
            this.BlockType = BlockType.Sign;
            this.Block = (Block)block;

            this._data = new BlockData
            {
                Text = text
            };
        }

        internal void SetLabel(LabelBlock block, string text, string textColor)
        {
            this.BlockType = BlockType.Label;
            this.Block = (Block)block;

            this._data = new BlockData
            {
                Text = text,
                TextColor = textColor
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

        /// <summary>
        ///     Determines whether the IBlockPlaceSendEvent has the same values as this block.
        /// </summary>
        /// <param name="other">The IBlockPlaceSendEvent to evaluate.</param>
        /// <returns></returns>
        /// <exception cref="System.NotSupportedException">The given send message is not supported.</exception>
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

        /// <summary>
        ///     Creates an <see cref="IBlockPlaceSendEvent" /> object with this block's data.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotSupportedException">The given send message is not supported.</exception>
        public IBlockPlaceSendEvent ToEvent()
        {
            switch (this.BlockType)
            {
                case BlockType.Normal:
                    return new BlockPlaceSendEvent(this.Layer, this.X, this.Y, this.Block);
                case BlockType.CoinDoor:
                    return new CoinDoorPlaceSendEvent(this.Layer, this.X, this.Y, (CoinDoorBlock)this.Block,
                        this.CoinsToCollect);
                case BlockType.Portal:
                    return new PortalPlaceSendEvent(this.Layer, this.X, this.Y, (PortalBlock)this.Block, this.PortalId,
                        this.PortalTarget, this.PortalRotation);
                case BlockType.Sound:
                    return new SoundPlaceSendEvent(this.Layer, this.X, this.Y, (SoundBlock)this.Block, this.SoundId);
                case BlockType.Sign:
                    return new SignPlaceSendEvent(this.Layer, this.X, this.Y, (SignBlock)this.Block, this.Text);
                case BlockType.Label:
                    return new LabelPlaceSendEvent(this.Layer, this.X, this.Y, (LabelBlock)this.Block, this.Text, this.TextColor);
                case BlockType.Rotatable:
                    return new RotatablePlaceSendEvent(this.Layer, this.X, this.Y, (RotatableBlock)this.Block,
                        this.Rotation);
                case BlockType.WorldPortal:
                    return new WorldPortalPlaceSendEvent(this.Layer, this.X, this.Y, (WorldPortalBlock)this.Block,
                        this.WorldPortalTarget);
                default:
                    throw new NotSupportedException("The given send message is not supported.");
            }
        }

// ReSharper disable once UnusedMember.Local
        private string DebuggerData()
        {
            switch (this.BlockType)
            {
                case BlockType.Normal:
                    return String.Format("Layer = {0}", this.Layer);
                case BlockType.CoinDoor:
                    return String.Format("CoinsToCollect = {0}", this.CoinsToCollect);
                case BlockType.Purple:
                    return String.Format("Purple ID = {0}", this.PurpleId);
                case BlockType.Death:
                    return String.Format("Deaths Required = {0}", this.DeathsRequired);
                case BlockType.Rotatable:
                    return String.Format("Rotation = {0}", this.Rotation);
                case BlockType.Sound:
                    return String.Format("SoundId = {0}", this.SoundId);
                case BlockType.Sign:
                    return String.Format("Text = {0}", this.Text);
                case BlockType.Label:
                    return String.Format("Text = {0}, TextColor = {1}", this.Text, this.TextColor);
                case BlockType.Portal:
                    return String.Format("Id = {0}, Target = {1}, Rotation = {2}", this.PortalId, this.PortalTarget,
                        this.PortalRotation);
                case BlockType.WorldPortal:
                    return String.Format("Target = {0}", this.WorldPortalTarget);
                default:
                    return String.Empty;
            }
        }

        /// <summary>
        ///     Clones this instance.
        /// </summary>
        /// <returns></returns>
        public WorldBlock Clone()
        {
            return new WorldBlock(this.MetadataPlatform, this.Layer, this.X, this.Y, this.Block)
            {
                _data = this._data.Clone(),
                BlockType = this.BlockType
            };
        }

        private struct BlockData
        {
            internal string Text { get; set; }
            internal string TextColor { get; set; }
            internal string WorldPortalTarget { get; set; }
            internal uint CoinsToCollect { get; set; }
            internal uint PurpleId { get; set; }
            internal uint DeathsRequired { get; set; }
            internal uint PortalId { get; set; }
            internal uint PortalTarget { get; set; }
            internal PortalRotation PortalRotation { get; set; }
            internal uint SoundId { get; set; }
            internal uint Rotation { get; set; }

            internal BlockData Clone()
            {
                return new BlockData
                {
                    Text = this.Text,
                    TextColor = this.TextColor,
                    WorldPortalTarget = this.WorldPortalTarget,
                    CoinsToCollect = this.CoinsToCollect,
                    PurpleId = this.PurpleId,
                    DeathsRequired = this.DeathsRequired,
                    PortalId = this.PortalId,
                    PortalTarget = this.PortalTarget,
                    PortalRotation = this.PortalRotation,
                    SoundId = this.SoundId,
                    Rotation = this.Rotation
                };
            }
        }
    }
}