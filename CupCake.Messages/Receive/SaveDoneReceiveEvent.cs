using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    ///     Occurs when the world is saved.
    /// </summary>
    public class SaveDoneReceiveEvent : ReceiveEvent
    {
        //No arguments

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReceiveEvent" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public SaveDoneReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}