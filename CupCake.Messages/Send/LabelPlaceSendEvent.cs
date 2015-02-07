using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    /// <summary>
    ///     Class Label Place Send Event
    /// </summary>
    public class LabelPlaceSendEvent : SignPlaceSendEvent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="LabelPlaceSendEvent" /> class.
        /// </summary>
        /// <param name="layer">The layer.</param>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="block">The block.</param>
        /// <param name="text">The text.</param>
        /// <param name="textColor">The text color.</param>
        public LabelPlaceSendEvent(Layer layer, int x, int y, string text, string textColor) 
            : base(layer, x, y, Block.DecorLabel, text)
        {
            this.TextColor = textColor;
        }


        /// <summary>
        ///     Gets or sets the text color.
        /// </summary>
        /// <value>
        ///     The text color.
        /// </value>
        public string TextColor { get; set; }

        /// <summary>
        ///     Gets the PlayerIO message representing the data in this <see cref="SendEvent" />.
        /// </summary>
        /// <returns></returns>
        public override Message GetMessage()
        {
            return Message.Create(this.Encryption, (int)this.Layer, this.X, this.Y, (int)this.Block, this.Text, this.TextColor);
        }
    }
}