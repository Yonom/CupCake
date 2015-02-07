using System;
using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class PurpleDoorPlaceReceiveEvent : ReceiveEvent, IBlockPlaceReceiveEvent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PurpleDoorPlaceReceiveEvent" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public PurpleDoorPlaceReceiveEvent(Message message)
            : base(message)
        {
            this.PosX = message.GetInteger(0);
            this.PosY = message.GetInteger(1);
            this.Block = (PurpleDoorBlock)message.GetInteger(2);
            this.PurpleId = message.GetUInt(3);
        }

        /// <summary>
        ///     Gets the layer.
        /// </summary>
        /// <value>The layer.</value>
        public Layer Layer
        {
            get { return Layer.Foreground; }
        }

        /// <summary>
        ///     Gets or sets the coin door block.
        /// </summary>
        /// <value>The block.</value>
        public PurpleDoorBlock Block { get; set; }

        /// <summary>
        ///     Gets or sets the purple id.
        /// </summary>
        /// <value>The purple id.</value>
        public uint PurpleId { get; set; }

        /// <summary>
        ///     Gets or sets the purple offset.
        /// </summary>
        /// <value>The purple offset.</value>
        public uint PurpleOffset { get; set; }

        /// <summary>
        ///     Gets or sets the position x of the player.
        /// </summary>
        /// <value>The position x.</value>
        public int PosX { get; set; }

        /// <summary>
        ///     Gets or sets the position y of the player.
        /// </summary>
        /// <value>The position y.</value>
        public int PosY { get; set; }

        /// <summary>
        ///     Gets or sets the layer.
        /// </summary>
        /// <value>The layer.</value>
        /// <exception cref="System.NotSupportedException">Can not set Layer on this kind of block</exception>
        Layer IBlockPlaceReceiveEvent.Layer
        {
            get { return this.Layer; }
            set { throw new NotSupportedException("Can not set Layer on this kind of block"); }
        }

        /// <summary>
        ///     Gets or sets the block.
        /// </summary>
        /// <value>The block.</value>
        Block IBlockPlaceReceiveEvent.Block
        {
            get { return (Block)this.Block; }
            set { this.Block = (PurpleDoorBlock)value; }
        }
    }
}
