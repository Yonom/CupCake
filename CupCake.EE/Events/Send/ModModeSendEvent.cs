using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public sealed class ModModeSendEvent : SendEvent
    {
        //No arguments

        public override Message GetMessage()
        {
            return Message.Create("mod");
        }
    }
}