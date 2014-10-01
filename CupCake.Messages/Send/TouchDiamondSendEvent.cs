using PlayerIOClient;

namespace CupCake.Messages.Send
{
    /// <summary>
    ///     Class Touch Diamond Send Event
    /// </summary>
    public class TouchDiamondSendEvent : SendEvent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TouchDiamondSendEvent" /> class.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        public TouchDiamondSendEvent(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

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
        ///     Gets the PlayerIO message representing the data in this <see cref="SendEvent" />.
        /// </summary>
        /// <returns></returns>
        public override Message GetMessage()
        {
            return Message.Create("diamondtouch", this.X, this.Y);
        }
    }
}