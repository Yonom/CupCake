using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    ///     Occurs when the level is reset, usually by using "/loadlevel" command.
    /// </summary>
    public class ResetReceiveEvent : ReceiveEvent
    {
        //No arguments

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReceiveEvent" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ResetReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}