using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    ///     Class PotionCountReceiveEvent.
    /// </summary>
    public class PotionCountReceiveEvent : ReceiveEvent
    {
        //No arguments

        /// <summary>
        ///     Initializes a new instance of the <see cref="PotionCountReceiveEvent" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public PotionCountReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}