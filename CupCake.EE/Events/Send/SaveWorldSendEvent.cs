using PlayerIOClient;

namespace CupCake.EE.Events.Send
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