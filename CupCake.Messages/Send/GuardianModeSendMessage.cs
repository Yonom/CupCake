using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class GuardianModeSendMessage  : SendEvent
    {
        public GuardianModeSendMessage(bool guardianModeEnabled)
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
