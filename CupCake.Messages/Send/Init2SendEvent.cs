using PlayerIOClient;

namespace CupCake.Messages.Send
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