using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class PotionReceiveMessage : ReceiveMessage
    {
        public bool Enabled { get; private set; }
        public Potion Potion { get; private set; }
        public int Timeout { get; private set; }
        public int UserId { get; private set; }

        public PotionReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.Potion = (Potion)message.GetInteger(1);
            this.Enabled = message.GetBoolean(2);
            this.Timeout = message.GetInteger(3);
        }
    }
}