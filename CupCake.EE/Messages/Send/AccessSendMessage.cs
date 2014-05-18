using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class AccessSendMessage : SendMessage
    {
        public readonly string EditKey;

        public AccessSendMessage(string editKey)
        {
            this.EditKey = editKey;
        }

        internal override Message GetMessage()
        {
            return Message.Create("access", this.EditKey);
        }
    }
}