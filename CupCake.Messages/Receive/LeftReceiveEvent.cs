using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    /// Class Left Receive Event.
    /// </summary>
    public class LeftReceiveEvent : ReceiveEvent, IUserReceiveEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LeftReceiveEvent"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public LeftReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }
    }
}