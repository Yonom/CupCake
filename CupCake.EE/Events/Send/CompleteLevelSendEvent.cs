using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public sealed class CompleteLevelSendEvent : SendEvent
    {
        //No arguments

        public override Message GetMessage()
        {
            return Message.Create("levelcomplete");
        }
    }
}