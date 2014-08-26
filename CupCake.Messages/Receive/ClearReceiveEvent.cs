using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    /// Class Clear Receive Event.
    /// </summary>
    public class ClearReceiveEvent : ReceiveEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClearReceiveEvent"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ClearReceiveEvent(Message message)
            : base(message)
        {
            this.RoomWidth = message.GetInteger(0);
            this.RoomHeight = message.GetInteger(1);
            this.BorderBlock = (Block)message.GetInteger(2);
            this.FillBlock = (Block)message.GetInteger(3);
        }

        /// <summary>
        /// Gets or sets the fill block.
        /// </summary>
        /// <value>The fill block.</value>
        public Block FillBlock { get; set; }
        /// <summary>
        /// Gets or sets the border block.
        /// </summary>
        /// <value>The border block.</value>
        public Block BorderBlock { get; set; }

        /// <summary>
        /// Gets or sets the height of the room.
        /// </summary>
        /// <value>The height of the room.</value>
        public int RoomHeight { get; set; }
        /// <summary>
        /// Gets or sets the width of the room.
        /// </summary>
        /// <value>The width of the room.</value>
        public int RoomWidth { get; set; }
    }
}