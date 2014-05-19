using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class GodModeSendMessage : SendMessage
    {
        public bool GodModeEnabled { get; set; }

        public GodModeSendMessage(bool godModeEnabled)
        {
            this.GodModeEnabled = godModeEnabled;
        }

        public override Message GetMessage()
        {
            return Message.Create("god", this.GodModeEnabled);
        }
    }
}