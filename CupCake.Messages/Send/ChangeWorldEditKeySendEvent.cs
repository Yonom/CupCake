using PlayerIOClient;

namespace CupCake.Messages.Send
{
    /// <summary>
    /// Class Change World Edit Key Send Event
    /// </summary>
    public class ChangeWorldEditKeySendEvent : SendEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeWorldEditKeySendEvent"/> class.
        /// </summary>
        /// <param name="editKey">The edit key.</param>
        public ChangeWorldEditKeySendEvent(string editKey)
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
            return Message.Create("key", this.EditKey);
        }
    }
}