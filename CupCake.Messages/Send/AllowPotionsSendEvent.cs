using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class AllowPotionsSendEvent : SendEvent
    {
        public AllowPotionsSendEvent(bool allowed)
        {
            this.Allowed = allowed;
        }

        public bool Allowed { get; set; }

        public override Message GetMessage()
        {
            return Message.Create("allowpotions", this.Allowed);
        }
    }
}