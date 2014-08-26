using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    /// Occurs when the player is killed.
    /// </summary>
    public class KillReceiveEvent : ReceiveEvent, IUserReceiveEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KillReceiveEvent"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public KillReceiveEvent(Message message)
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