using PlayerIOClient;

namespace CupCake.EE.Events.Send
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