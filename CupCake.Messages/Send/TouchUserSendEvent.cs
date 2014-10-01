using CupCake.Messages.User;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    /// <summary>
    ///     Touch User Send Event
    /// </summary>
    public class TouchUserSendEvent : SendEvent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TouchUserSendEvent" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="reason">The reason (transferred potion).</param>
        public TouchUserSendEvent(int userId, Potion reason)
        {
            this.UserId = userId;
            this.Reason = reason;
        }

        /// <summary>
        ///     Gets or sets the reason (transferred potion).
        /// </summary>
        /// <value>
        ///     The reason (transferred potion).
        /// </value>
        public Potion Reason { get; set; }

        /// <summary>
        ///     Gets or sets the user identifier.
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        ///     Gets the PlayerIO message representing the data in this <see cref="SendEvent" />.
        /// </summary>
        /// <returns></returns>
        public override Message GetMessage()
        {
            return Message.Create("touch", this.UserId, this.Reason);
        }
    }
}