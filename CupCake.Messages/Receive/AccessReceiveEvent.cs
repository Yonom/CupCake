using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    /// Class Access Receive Event.
    /// </summary>
    public class AccessReceiveEvent : ReceiveEvent
    {
        //No arguments

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessReceiveEvent"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public AccessReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}