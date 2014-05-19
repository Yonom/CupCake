using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public class AllowPotionsReceiveMessage : ReceiveMessage
    {
        public AllowPotionsReceiveMessage(Message message)
            : base(message)
        {
            this.Allowed = message.GetBoolean(0);
        }

        public bool Allowed { get; private set; }
    }
}