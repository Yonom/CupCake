using PlayerIOClient;

namespace CupCake.Messages.Send
{
    /// <summary>
    /// Class Access Send Event.
    /// </summary>
    public class AccessSendEvent : SendEvent
    {
        public AccessSendEvent(string editKey)
        {
            this.EditKey = editKey;
        }

        /// <summary>
        /// Gets or sets the edit key.
        /// </summary>
        /// <value>
        /// The edit key.
        /// </value>
        public string EditKey { get; set; }

        /// <summary>
        /// Gets the PlayerIO message representing the data in this <see cref="SendEvent" />.
        /// </summary>
        /// <returns></returns>
        public override Message GetMessage()
        {
            return Message.Create("access", this.EditKey);
        }
    }
}