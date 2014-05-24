using PlayerIOClient;

namespace CupCake.Messages.Events.Send
{
    public class GodModeSendEvent : SendEvent
    {
        public GodModeSendEvent(bool godModeEnabled)
        {
            this.GodModeEnabled = godModeEnabled;
        }

        public bool GodModeEnabled { get; set; }

        public override Message GetMessage()
        {
            return Message.Create("god", this.GodModeEnabled);
        }
    }
}