using PlayerIOClient;

namespace CupCake.Messages.Events.Send
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