using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public sealed class PotionReceiveEvent : ReceiveEvent
    {
        public PotionReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.Potion = (Potion)message.GetInteger(1);
            this.Enabled = message.GetBoolean(2);
            this.Timeout = message.GetInteger(3);
        }

        public bool Enabled { get; private set; }
        public Potion Potion { get; private set; }
        public int Timeout { get; private set; }
        public int UserId { get; private set; }
    }
}