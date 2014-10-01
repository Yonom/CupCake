using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    ///     Class Auto-Text Receive Event.
    /// </summary>
    public class AutoTextReceiveEvent : ReceiveEvent, IUserReceiveEvent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AutoTextReceiveEvent" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public AutoTextReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.AutoText = message.GetString(1);
        }

        /// <summary>
        ///     Gets or sets the automatic text value.
        /// </summary>
        /// <value>The automatic text.</value>
        public string AutoText { get; set; }

        /// <summary>
        ///     Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }
    }
}