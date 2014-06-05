using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class SaveWorldSendEvent : SendEvent
    {
        //No arguments

        public override Message GetMessage()
        {
            return Message.Create("save");
        }
    }
}