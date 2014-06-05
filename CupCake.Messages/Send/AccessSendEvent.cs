using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class AccessSendEvent : SendEvent
    {
        public AccessSendEvent(string editKey)
        {
            this.EditKey = editKey;
        }

        public string EditKey { get; set; }

        public override Message GetMessage()
        {
            return Message.Create("access", this.EditKey);
        }
    }
}