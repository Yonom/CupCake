using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    ///     Occurs when a player gives the world a woot.
    /// </summary>
    public class WootUpReceiveEvent : ReceiveEvent, IUserReceiveEvent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ReceiveEvent" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public WootUpReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }

        /// <summary>
        ///     Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }
    }
}