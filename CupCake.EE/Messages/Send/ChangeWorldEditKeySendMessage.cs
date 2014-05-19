using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class ChangeWorldEditKeySendMessage : SendMessage
    {
        public string EditKey { get; set; }

        public ChangeWorldEditKeySendMessage(string editKey)
        {
            this.EditKey = editKey;
        }

        public override Message GetMessage()
        {
            return Message.Create("key", this.EditKey);
        }
    }
}