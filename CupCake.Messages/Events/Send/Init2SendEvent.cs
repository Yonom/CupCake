using PlayerIOClient;

namespace CupCake.Messages.Events.Send
{
    public class Init2SendEvent : SendEvent
    {
        //No arguments

        public override Message GetMessage()
        {
            return Message.Create("init2");
        }
    }
}