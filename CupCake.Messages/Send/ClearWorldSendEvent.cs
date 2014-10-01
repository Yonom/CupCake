using PlayerIOClient;

namespace CupCake.Messages.Send
{
    /// <summary>
    ///     Class Clear World Send Event
    /// </summary>
    public class ClearWorldSendEvent : SendEvent
    {
        //No arguments

        /// <summary>
        ///     Gets the PlayerIO message representing the data in this <see cref="SendEvent" />.
        /// </summary>
        /// <returns></returns>
        public override Message GetMessage()
        {
            return Message.Create("clear");
        }
    }
}