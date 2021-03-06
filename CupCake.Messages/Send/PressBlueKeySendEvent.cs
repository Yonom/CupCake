using PlayerIOClient;

namespace CupCake.Messages.Send
{
    /// <summary>
    ///     Class Press Blue Key Send Event
    /// </summary>
    public class PressBlueKeySendEvent : SendEvent, IEncryptedSendEvent
    {
        /// <summary>
        ///     Gets or sets the encryption string.
        /// </summary>
        /// <value>
        ///     The encryption string.
        /// </value>
        public string Encryption { get; set; }

        /// <summary>
        ///     Gets the PlayerIO message representing the data in this <see cref="SendEvent" />.
        /// </summary>
        /// <returns></returns>
        public override Message GetMessage()
        {
            return Message.Create(this.Encryption + "b");
        }
    }
}