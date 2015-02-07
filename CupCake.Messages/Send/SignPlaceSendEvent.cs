using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    /// <summary>
    ///     Class Sign Place Send Event
    /// </summary>
    public class SignPlaceSendEvent : SendEvent, IBlockPlaceSendEvent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SignPlaceSendEvent" /> class.
        /// </summary>
        /// <param name="layer">The layer.</param>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="block">The block.</param>
        /// <param name="text">The text.</param>
        public SignPlaceSendEvent(Layer layer, int x, int y, string text)
        {
            this.Block = Block.DecorSign;
            this.X = x;
            this.Y = y;
            this.Layer = BlockUtils.CorrectLayer(Block, layer);

            this.Text = text;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SignPlaceSendEvent" /> class.
        /// </summary>
        /// <param name="layer">The layer.</param>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="block">The block.</param>
        /// <param name="text">The text.</param>
        protected SignPlaceSendEvent(Layer layer, int x, int y, Block block, string text)
        {
            this.Block = block;
            this.X = x;
            this.Y = y;
            this.Layer = BlockUtils.CorrectLayer(Block, layer);

            this.Text = text;
        }

        /// <summary>
        ///     Gets or sets the block.
        /// </summary>
        /// <value>
        ///     The block.
        /// </value>
        public Block Block { get; set; }

        /// <summary>
        ///     Gets or sets the text.
        /// </summary>
        /// <value>
        ///     The text.
        /// </value>
        public string Text { get; set; }

        Block IBlockPlaceSendEvent.Block
        {
            get { return (Block)this.Block; }
            set { this.Block = (Block)value; }
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
            return Message.Create(this.Encryption, (int)this.Layer, this.X, this.Y, (int)this.Block, this.Text);
        }
    }
}