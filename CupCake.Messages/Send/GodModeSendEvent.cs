using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class GodModeSendEvent : SendEvent
    {
        public GodModeSendEvent(bool godModeEnabled)
        {
            GodModeEnabled = godModeEnabled;
        }

        public bool GodModeEnabled { get; set; }

        public override Message GetMessage()
        {
            return Message.Create("god", GodModeEnabled);
        }
    }
}