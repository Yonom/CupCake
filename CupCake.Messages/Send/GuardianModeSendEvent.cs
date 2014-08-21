using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class GuardianModeSendEvent : SendEvent
    {
        public GuardianModeSendEvent(bool guardianModeEnabled)
        {
            this.GuardianModeEnabled = guardianModeEnabled;
        }

        public bool GuardianModeEnabled { get; set; }

        public override Message GetMessage()
        {
            return Message.Create("guardian", this.GuardianModeEnabled);
        }
    }
}