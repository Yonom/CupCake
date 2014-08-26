using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    /// Class Give Witch Receive Event.
    /// </summary>
    public class GiveWitchReceiveEvent : ReceiveEvent
    {
        //No arguments

        /// <summary>
        /// Initializes a new instance of the <see cref="GiveWitchReceiveEvent"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public GiveWitchReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}