using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    /// Class Crown Receive Event.
    /// </summary>
    public class CrownReceiveEvent : ReceiveEvent, IUserReceiveEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrownReceiveEvent"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public CrownReceiveEvent(Message message)
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