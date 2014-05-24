using PlayerIOClient;

namespace CupCake.Messages.Events.Send
{
    public class ChangeWorldEditKeySendEvent : SendEvent
    {
        public ChangeWorldEditKeySendEvent(string editKey)
        {
            this.EditKey = editKey;
        }

        public string EditKey { get; set; }

        public override Message GetMessage()
        {
            return Message.Create("key", this.EditKey);
        }
    }
}