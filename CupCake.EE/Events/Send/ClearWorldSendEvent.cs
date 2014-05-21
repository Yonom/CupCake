using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public class ClearWorldSendEvent : SendEvent
    {
        //No arguments

        public override Message GetMessage()
        {
            return Message.Create("clear");
        }
    }
}