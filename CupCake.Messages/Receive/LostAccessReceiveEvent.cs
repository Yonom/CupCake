using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    /// Occurs when a player looses edit access to the world.
    /// </summary>
    public class LostAccessReceiveEvent : ReceiveEvent
    {
        //No arguments

        /// <summary>
        /// Initializes a new instance of the <see cref="LostAccessReceiveEvent"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public LostAccessReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}