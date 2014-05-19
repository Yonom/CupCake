using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class AccessSendMessage : SendMessage
    {
        public AccessSendMessage(string editKey)
        {
            this.EditKey = editKey;
        }

        public string EditKey { get; set; }

        public override Message GetMessage()
        {
            return Message.Create("access", this.EditKey);
        }
    }
}