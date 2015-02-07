using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class PurpleDoorPlaceSendEvent : SendEvent, IBlockPlaceSendEvent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CoinDoorPlaceSendEvent" /> class.
        /// </summary>
        /// <param name="layer">The layer.</param>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="block">The block.</param>
        /// <param name="purpleId">The purple id.</param>
        public PurpleDoorPlaceSendEvent(Layer layer, int x, int y, PurpleDoorBlock block, uint purpleId)
        {
            this.Block = block;
            this.X = x;
            this.Y = y;
            this.Layer = BlockUtils.CorrectLayer((Block)block, layer);

            this.PurpleId = purpleId;
        }

        /// <summary>
        ///     Gets or sets the block.
        /// </summary>
        /// <value>
        ///     The block.
        /// </value>
        public PurpleDoorBlock Block { get; set; }

        /// <summary>
        ///     Gets or sets the purple id.
        /// </summary>
        /// <value>
        ///     The purple id.
        /// </value>
        public uint PurpleId { get; set; }

        Block IBlockPlaceSendEvent.Block
        {
            get { return (Block)this.Block; }
            set { this.Block = (PurpleDoorBlock)value; }
        }

        /// <summary>
        ///     Gets or sets the layer.
        /// </summary>
        /// <value>
        ///     The layer.
        /// </value>
        public Layer Layer { get; set; }

        /// <summary>
        ///     Gets or sets the x-coordinate.
        /// </summary>
        /// <value>
        ///     The x-coordinate.
        /// </value>
        public int X { get; set; }

        /// <summary>
        ///     Gets or sets the y-coordinate.
        /// </summary>
        /// <value>
        ///     The y-coordinate.
        /// </value>
        public int Y { get; set; }

        /// <summary>
        ///     Gets or sets the encryption string.
        /// </summary>
        /// <value>
        ///     The encryption string.
        /// </value>
        public string Encryption { get; set; }

        /// <summary>
        ///     Gets the PlayerIO message representing the data in this <see cref="SendEvent" />.
        /// </summary>
        /// <returns></returns>
        public override Message GetMessage()
        {
            return Message.Create(this.Encryption, (int)this.Layer, this.X, this.Y, (int)this.Block, this.PurpleId);
        }
    }
}
