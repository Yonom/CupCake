using PlayerIOClient;

namespace CupCake.Messages.Send
{
    /// <summary>
    /// Class Allow Potions Send Event
    /// </summary>
    public class AllowPotionsSendEvent : SendEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AllowPotionsSendEvent"/> class.
        /// </summary>
        /// <param name="allowed">if set to <c>true</c> then potions are allowed.</param>
        public AllowPotionsSendEvent(bool allowed)
        {
            this.Allowed = allowed;
        }

        /// <summary>
        /// Gets or sets a value indicating whether potions are allowed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if potions are allowed; otherwise, <c>false</c>.
        /// </value>
        public bool Allowed { get; set; }

        /// <summary>
        /// Gets the PlayerIO message representing the data in this <see cref="SendEvent" />.
        /// </summary>
        /// <returns></returns>
        public override Message GetMessage()
        {
            return Message.Create("allowpotions", this.Allowed);
        }
    }
}