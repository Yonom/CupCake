using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class ChangeWorldNameSendMessage : SendMessage
    {
        public ChangeWorldNameSendMessage(string worldName)
        {
            this.WorldName = worldName;
        }

        public string WorldName { get; set; }

        public override Message GetMessage()
        {
            return Message.Create("name", this.WorldName);
        }
    }
}