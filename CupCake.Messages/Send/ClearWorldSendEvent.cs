using PlayerIOClient;

namespace CupCake.Messages.Send
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