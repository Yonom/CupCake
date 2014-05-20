using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public sealed class Init2SendEvent : SendEvent
    {
        //No arguments

        public override Message GetMessage()
        {
            return Message.Create("init2");
        }
    }
}