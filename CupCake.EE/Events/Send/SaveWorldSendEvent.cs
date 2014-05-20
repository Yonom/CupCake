using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public sealed class SaveWorldSendEvent : SendEvent
    {
        //No arguments

        public override Message GetMessage()
        {
            return Message.Create("save");
        }
    }
}