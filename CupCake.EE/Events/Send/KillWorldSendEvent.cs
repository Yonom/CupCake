using PlayerIOClient;

namespace CupCake.EE.Events.Send
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