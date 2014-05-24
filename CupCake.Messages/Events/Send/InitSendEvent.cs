using PlayerIOClient;

namespace CupCake.Messages.Events.Send
{
    public class InitSendEvent : SendEvent
    {
        //No arguments

        public override Message GetMessage()
        {
            return Message.Create("init");
        }
    }
}