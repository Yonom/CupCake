using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class ModModeSendEvent : SendEvent
    {
        //No arguments

        public override Message GetMessage()
        {
            return Message.Create("mod");
        }
    }
}