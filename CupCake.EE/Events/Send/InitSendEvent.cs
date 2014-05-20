using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public sealed class InitSendEvent : SendEvent
    {
        //No arguments

        public override Message GetMessage()
        {
            return Message.Create("init");
        }
    }
}