using PlayerIOClient;

namespace CupCake.Messages.Send
{
    /// <summary>
    ///     Class Change World Name Send Event
    /// </summary>
    public class ChangeWorldNameSendEvent : SendEvent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ChangeWorldNameSendEvent" /> class.
        /// </summary>
        /// <param name="worldName">Name of the world.</param>
        public ChangeWorldNameSendEvent(string worldName)
        {
            this.WorldName = worldName;
        }

        /// <summary>
        ///     Gets or sets the name of the world.
        /// </summary>
        /// <value>
        ///     The name of the world.
        /// </value>
        public string WorldName { get; set; }

        /// <summary>
        ///     Gets the PlayerIO message representing the data in this <see cref="SendEvent" />.
        /// </summary>
        /// <returns></returns>
        public override Message GetMessage()
        {
            return Message.Create("name", this.WorldName);
        }
    }
}