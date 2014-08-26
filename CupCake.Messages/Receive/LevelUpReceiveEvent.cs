using CupCake.Messages.User;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    /// Occurs when a player levels up.
    /// </summary>
    public class LevelUpReceiveEvent : ReceiveEvent, IUserReceiveEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LevelUpReceiveEvent"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public LevelUpReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.NewClass = (MagicClass)message.GetInteger(1);
        }

        /// <summary>
        /// Gets or sets the new class.
        /// </summary>
        /// <value>The new class.</value>
        public MagicClass NewClass { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }
    }
}