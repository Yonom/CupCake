using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class ChangeWorldNameSendMessage : SendMessage
    {
        public readonly string WorldName;

        public ChangeWorldNameSendMessage(string worldName)
        {
            this.WorldName = worldName;
        }

        internal override Message GetMessage()
        {
            return Message.Create("name", this.WorldName);
        }
    }
}