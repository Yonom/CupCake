using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    /// Occurs when the server version has increased.
    /// </summary>
    public class UpgradeReceiveEvent : ReceiveEvent
    {
        //No arguments

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiveEvent" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public UpgradeReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}