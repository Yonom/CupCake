using System;
using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class DeathDoorPlaceReceiveEvent : ReceiveEvent, IBlockPlaceReceiveEvent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DeathDoorPlaceReceiveEvent" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public DeathDoorPlaceReceiveEvent(Message message)
            : base(message)
        {
            this.PosX = message.GetInteger(0);
            this.PosY = message.GetInteger(1);
            this.Block = (DeathDoorBlock)message.GetInteger(2);
            this.DeathsRequired = message.GetUInt(3);
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
        public DeathDoorBlock Block { get; set; }

        /// <summary>
        ///     Gets or sets the deaths required.
        /// </summary>
        /// <value>The amount of deaths required.</value>
        public uint DeathsRequired { get; set; }

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
            set { this.Block = (DeathDoorBlock)value; }
        }
    }
}
