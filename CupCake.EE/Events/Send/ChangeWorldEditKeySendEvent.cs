using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public sealed class ChangeWorldEditKeySendEvent : SendEvent
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