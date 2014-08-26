using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    /// Occurs when a player writes on a sign in the world.
    /// </summary>
    public class WriteReceiveEvent : ReceiveEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiveEvent" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public WriteReceiveEvent(Message message)
            : base(message)
        {
            this.Title = message.GetString(0);
            this.Text = message.GetString(1);
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }
    }
}