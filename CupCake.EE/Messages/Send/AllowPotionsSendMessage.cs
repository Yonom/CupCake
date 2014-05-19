using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public class AllowPotionsSendMessage : SendMessage
    {
        public AllowPotionsSendMessage(bool allowed)
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