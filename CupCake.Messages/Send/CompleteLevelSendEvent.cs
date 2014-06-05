using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class CompleteLevelSendEvent : SendEvent
    {
        //No arguments

        public override Message GetMessage()
        {
            return Message.Create("levelcomplete");
        }
    }
}