using PlayerIOClient;

namespace CupCake.Messages.Events.Send
{
    public class WootUpSendEvent : SendEvent
    {
        //No arguments

        public override Message GetMessage()
        {
            return Message.Create("wootup");
        }
    }
}