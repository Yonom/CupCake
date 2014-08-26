using CupCake.Messages.User;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    /// Class Face Receive Event.
    /// </summary>
    public class FaceReceiveEvent : ReceiveEvent, IUserReceiveEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FaceReceiveEvent"/> class.
        /// </summary>
        /// <param name="message">The EE message.</param>
        public FaceReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.Face = (Smiley)message.GetInteger(1);
        }

        /// <summary>
        /// Gets or sets the face.
        /// </summary>
        /// <value>The face.</value>
        public Smiley Face { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }
    }
}