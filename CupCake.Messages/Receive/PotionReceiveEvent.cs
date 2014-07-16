using CupCake.Messages.User;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class PotionReceiveEvent : ReceiveEvent, IUserReceiveEvent
    {
        public PotionReceiveEvent(Message message)
            : base(message)
        {
            UserId = message.GetInteger(0);
            Potion = (Potion) message.GetInteger(1);
            Enabled = message.GetBoolean(2);
            Timeout = message.GetInteger(3);
        }

        public bool Enabled { get; set; }
        public Potion Potion { get; set; }
        public int Timeout { get; set; }
        public int UserId { get; set; }
    }
}