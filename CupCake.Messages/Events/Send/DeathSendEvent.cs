using PlayerIOClient;

namespace CupCake.Messages.Events.Send
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