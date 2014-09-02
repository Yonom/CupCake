using CupCake.Messages.User;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    /// <summary>
    /// Class Change Face Send Event
    /// </summary>
    public class ChangeFaceSendEvent : SendEvent, IEncryptedSendEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeFaceSendEvent"/> class.
        /// </summary>
        /// <param name="face">The face.</param>
        public ChangeFaceSendEvent(Smiley face)
        {
            this.Face = face;
        }

        /// <summary>
        /// Gets or sets the face.
        /// </summary>
        /// <value>
        /// The face.
        /// </value>
        public Smiley Face { get; set; }

        /// <summary>
        /// Gets or sets the encryption string.
        /// </summary>
        /// <value>
        /// The encryption string.
        /// </value>
        public string Encryption { get; set; }

        /// <summary>
        /// Gets the PlayerIO message representing the data in this <see cref="SendEvent" />.
        /// </summary>
        /// <returns></returns>
        public override Message GetMessage()
        {
            return Message.Create(this.Encryption + "f", this.Face);
        }
    }
}