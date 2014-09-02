using PlayerIOClient;

namespace CupCake.Messages.Send
{
    /// <summary>
    /// Class Checkpoint Send Event
    /// </summary>
    public class CheckpointSendEvent : SendEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckpointSendEvent"/> class.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        public CheckpointSendEvent(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

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
        /// Gets the PlayerIO message representing the data in this <see cref="SendEvent" />.
        /// </summary>
        /// <returns></returns>
        public override Message GetMessage()
        {
            return Message.Create("checkpoint", this.X, this.Y);
        }
    }
}