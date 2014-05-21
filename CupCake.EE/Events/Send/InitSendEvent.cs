using PlayerIOClient;

namespace CupCake.EE.Events.Send
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