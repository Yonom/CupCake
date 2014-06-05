using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class PotionReceiveEvent : ReceiveEvent, IUserEvent
    {
        public PotionReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.Potion = (Potion)message.GetInteger(1);
            this.Enabled = message.GetBoolean(2);
            this.Timeout = message.GetInteger(3);
        }

        public bool Enabled { get; set; }
        public Potion Potion { get; set; }
        public int Timeout { get; set; }
        public int UserId { get; set; }
    }
}