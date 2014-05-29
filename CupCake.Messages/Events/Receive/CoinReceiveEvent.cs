using PlayerIOClient;

namespace CupCake.Messages.Events.Receive
{
    public class CoinReceiveEvent : ReceiveEvent, IUserEvent
    {
        public CoinReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.Coins = message.GetInteger(1);
        }

        public int Coins { get; set; }
        public int UserId { get; set; }
    }
}