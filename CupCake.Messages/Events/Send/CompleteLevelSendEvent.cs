using PlayerIOClient;

namespace CupCake.Messages.Events.Send
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