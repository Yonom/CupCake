using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class ChangeWorldEditKeySendEvent : SendEvent
    {
        public ChangeWorldEditKeySendEvent(string editKey)
        {
            EditKey = editKey;
        }

        public string EditKey { get; set; }

        public override Message GetMessage()
        {
            return Message.Create("key", EditKey);
        }
    }
}