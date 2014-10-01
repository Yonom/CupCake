using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    ///     Class Give Grinch Receive Event.
    /// </summary>
    public class GiveGrinchReceiveEvent : ReceiveEvent
    {
        //No arguments

        /// <summary>
        ///     Initializes a new instance of the <see cref="GiveGrinchReceiveEvent" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public GiveGrinchReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}