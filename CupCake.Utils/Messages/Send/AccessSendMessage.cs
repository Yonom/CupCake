using PlayerIOClient;

namespace CupCake.Utils.Messages.Send
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