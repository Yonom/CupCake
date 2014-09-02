using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    /// <summary>
    /// Class Portal Place Send Event
    /// </summary>
    public class PortalPlaceSendEvent : SendEvent, IBlockPlaceSendEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PortalPlaceSendEvent"/> class.
        /// </summary>
        /// <param name="layer">The layer.</param>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="block">The block.</param>
        /// <param name="portalId">The portal identifier.</param>
        /// <param name="portalTarget">The portal target.</param>
        /// <param name="portalRotation">The portal rotation.</param>
        public PortalPlaceSendEvent(Layer layer, int x, int y, PortalBlock block, uint portalId, uint portalTarget,
            PortalRotation portalRotation)
        {
            this.Block = block;
            this.X = x;
            this.Y = y;
            this.Layer = BlockUtils.CorrectLayer((Block)block, layer);

            this.PortalId = portalId;
            this.PortalTarget = portalTarget;
            this.PortalRotation = portalRotation;
        }

        /// <summary>
        /// Gets or sets the block.
        /// </summary>
        /// <value>
        /// The block.
        /// </value>
        public PortalBlock Block { get; set; }

        /// <summary>
        /// Gets or sets the portal rotation.
        /// </summary>
        /// <value>
        /// The portal rotation.
        /// </value>
        public PortalRotation PortalRotation { get; set; }

        /// <summary>
        /// Gets or sets the portal identifier.
        /// </summary>
        /// <value>
        /// The portal identifier.
        /// </value>
        public uint PortalId { get; set; }

        /// <summary>
        /// Gets or sets the portal target.
        /// </summary>
        /// <value>
        /// The portal target.
        /// </value>
        public uint PortalTarget { get; set; }

        Block IBlockPlaceSendEvent.Block
        {
            get { return (Block)this.Block; }
            set { this.Block = (PortalBlock)value; }
        }

        /// <summary>
        /// Gets or sets the layer.
        /// </summary>
        /// <value>
        /// The layer.
        /// </value>
        public Layer Layer { get; set; }

        /// <summary>
        /// Gets or sets the x-coordinate.
        /// </summary>
        /// <value>
        /// The x-coordinate.
        /// </value>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the y-coordinate.
        /// </summary>
        /// <value>
        /// The y-coordinate.
        /// </value>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the encryption string.
        /// </summary>
        /// <value>
        /// The encryption string.
        /// </value>
        public string Encryption { get; set; }

        /// <summary>
        /// Gets the PlayerIO message representing the data in this <see cref="SendEvent" />.
        /// </summary>
        /// <returns></returns>
        public override Message GetMessage()
        {
            return Message.Create(this.Encryption, (int)this.Layer, this.X, this.Y, (int)this.Block,
                (uint)this.PortalRotation, this.PortalId, this.PortalTarget);
        }
    }
}