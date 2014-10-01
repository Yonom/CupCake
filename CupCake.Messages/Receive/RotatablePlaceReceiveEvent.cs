using System;
using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    ///     Occurs when a rotatable block is placed.
    /// </summary>
    public class RotatablePlaceReceiveEvent : ReceiveEvent, IBlockPlaceReceiveEvent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ReceiveEvent" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public RotatablePlaceReceiveEvent(Message message)
            : base(message)
        {
            this.PosX = message.GetInteger(0);
            this.PosY = message.GetInteger(1);
            this.Block = (RotatableBlock)message.GetInteger(2);
            this.Rotation = message.GetUInt(3);
        }

        /// <summary>
        ///     Gets or sets the layer.
        /// </summary>
        /// <value>The layer.</value>
        public Layer Layer
        {
            get { return Layer.Foreground; }
        }

        /// <summary>
        ///     Gets or sets the block.
        /// </summary>
        /// <value>The block.</value>
        public RotatableBlock Block { get; set; }

        /// <summary>
        ///     Gets or sets the rotation.
        /// </summary>
        /// <value>The rotation.</value>
        public uint Rotation { get; set; }

        /// <summary>
        ///     Gets or sets the position x.
        /// </summary>
        /// <value>The position x.</value>
        public int PosX { get; set; }

        /// <summary>
        ///     Gets or sets the position y.
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
            set { this.Block = (RotatableBlock)value; }
        }
    }
}