using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public class AllowPotionsSendMessage : SendMessage
    {
        public bool Allowed { get; set; }

        public AllowPotionsSendMessage(bool allowed)
        {
            this.Allowed = allowed;
        }

        public override Message GetMessage()
        {
            return Message.Create("allowpotions", this.Allowed);
        }
    }
}