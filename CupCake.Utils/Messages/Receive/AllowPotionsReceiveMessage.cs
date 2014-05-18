using PlayerIOClient;

namespace CupCake.Utils.Messages.Receive
{
    public class AllowPotionsReceiveMessage : ReceiveMessage
    {
        public readonly bool Allowed;

        internal AllowPotionsReceiveMessage(Message message)
            : base(message)
        {
            this.Allowed = message.GetBoolean(0);
        }
    }
}