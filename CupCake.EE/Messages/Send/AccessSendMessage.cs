using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class AccessSendMessage : SendMessage
    {
        public string EditKey { get; set; }

        public AccessSendMessage(string editKey)
        {
            this.EditKey = editKey;
        }

        public override Message GetMessage()
        {
            return Message.Create("access", this.EditKey);
        }
    }
}