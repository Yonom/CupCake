using PlayerIOClient;

namespace CupCake.EE.Events.Send
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