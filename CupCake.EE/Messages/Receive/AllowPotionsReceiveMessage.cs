using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public class AllowPotionsReceiveMessage : ReceiveMessage
    {
        public bool Allowed { get; private set; }

        public AllowPotionsReceiveMessage(Message message)
            : base(message)
        {
            this.Allowed = message.GetBoolean(0);
        }
    }
}