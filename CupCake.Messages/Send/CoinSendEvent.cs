using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class CoinSendEvent : SendEvent
    {
        public CoinSendEvent(int coins, int coinX, int coinY)
        {
            Coins = coins;
            CoinX = coinX;
            CoinY = coinY;
        }

        public int CoinX { get; set; }
        public int CoinY { get; set; }
        public int Coins { get; set; }

        public override Message GetMessage()
        {
            return Message.Create("c", Coins, CoinX, CoinY);
        }
    }
}