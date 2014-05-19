using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class GodModeSendMessage : SendMessage
    {
        public GodModeSendMessage(bool godModeEnabled)
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