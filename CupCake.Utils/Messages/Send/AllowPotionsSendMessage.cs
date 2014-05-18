using PlayerIOClient;

namespace CupCake.Utils.Messages.Send
{
    public class AllowPotionsSendMessage : SendMessage
    {
        public readonly bool Allowed;

        public AllowPotionsSendMessage(bool allowed)
        {
            this.Allowed = allowed;
        }

        internal override Message GetMessage()
        {
            return Message.Create("allowpotions", this.Allowed);
        }
    }
}