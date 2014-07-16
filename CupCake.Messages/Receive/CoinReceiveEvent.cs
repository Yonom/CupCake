using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class CoinReceiveEvent : ReceiveEvent, IUserReceiveEvent
    {
        public CoinReceiveEvent(Message message)
            : base(message)
        {
            UserId = message.GetInteger(0);
            Coins = message.GetInteger(1);
        }

        public int Coins { get; set; }
        public int UserId { get; set; }
    }
}