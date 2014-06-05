using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class KillWorldSendEvent : SendEvent
    {
        //No arguments

        public override Message GetMessage()
        {
            return Message.Create("kill");
        }
    }
}