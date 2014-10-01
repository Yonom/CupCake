using PlayerIOClient;

namespace CupCake.Messages.Send
{
    /// <summary>
    ///     Class Init2 Send Event
    /// </summary>
    public class Init2SendEvent : SendEvent
    {
        //No arguments

        /// <summary>
        ///     Gets the PlayerIO message representing the data in this <see cref="SendEvent" />.
        /// </summary>
        /// <returns></returns>
        public override Message GetMessage()
        {
            return Message.Create("init2");
        }
    }
}