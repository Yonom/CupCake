using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    /// Occurs when the player enters or exits guardian mode.
    /// </summary>
    public class GuardianModeReceiveEvent : ReceiveEvent, IUserReceiveEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GuardianModeReceiveEvent"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public GuardianModeReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.IsGuardian = message.GetBoolean(1);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this player is in guardian mode.
        /// </summary>
        /// <value><c>true</c> if this player is in guardian mode; otherwise, <c>false</c>.</value>
        public bool IsGuardian { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }
    }
}