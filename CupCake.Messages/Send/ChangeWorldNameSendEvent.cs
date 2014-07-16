using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class ChangeWorldNameSendEvent : SendEvent
    {
        public ChangeWorldNameSendEvent(string worldName)
        {
            WorldName = worldName;
        }

        public string WorldName { get; set; }

        public override Message GetMessage()
        {
            return Message.Create("name", WorldName);
        }
    }
}