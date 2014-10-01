using CupCake.Messages.User;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    /// <summary>
    ///     Class Potion Send Event
    /// </summary>
    public class PotionSendEvent : SendEvent, IEncryptedSendEvent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PotionSendEvent" /> class.
        /// </summary>
        /// <param name="potion">The potion.</param>
        public PotionSendEvent(Potion potion)
        {
            this.Potion = potion;
        }

        /// <summary>
        ///     Gets or sets the potion.
        /// </summary>
        /// <value>
        ///     The potion.
        /// </value>
        public Potion Potion { get; set; }

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
            return Message.Create(this.Encryption + "p", (int)this.Potion);
        }
    }
}