using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public class DeathSendEvent : SendEvent
    {
        //No arguments

        public override Message GetMessage()
        {
            return Message.Create("death");
        }
    }
}