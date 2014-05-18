using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public class AllowPotionsSendMessage : SendMessage
    {
        public readonly bool Allowed;

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