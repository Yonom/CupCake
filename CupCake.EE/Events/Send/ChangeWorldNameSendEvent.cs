using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public sealed class ChangeWorldNameSendEvent : SendEvent
    {
        public ChangeWorldNameSendEvent(string worldName)
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