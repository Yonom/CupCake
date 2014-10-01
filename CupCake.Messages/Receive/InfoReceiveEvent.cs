using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    ///     Occurs when the server sends information pertaining to low-level functions like (a) you were kicked or (b) the room
    ///     is full or (c) rate limit exceeded.
    /// </summary>
    public class InfoReceiveEvent : ReceiveEvent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InfoReceiveEvent" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public InfoReceiveEvent(Message message)
            : base(message)
        {
            this.Title = message.GetString(0);
            this.Text = message.GetString(1);
        }

        /// <summary>
        ///     Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }

        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }
    }
}