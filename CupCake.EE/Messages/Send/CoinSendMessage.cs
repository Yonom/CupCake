using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class CoinSendMessage : SendMessage
    {
        public int CoinX { get; set; }
        public int CoinY { get; set; }
        public int Coins { get; set; }

        public CoinSendMessage(int coins, int coinX, int coinY)
        {
            this.Coins = coins;
            this.CoinX = coinX;
            this.CoinY = coinY;
        }

        public override Message GetMessage()
        {
            return Message.Create("c", this.Coins, this.CoinX, this.CoinY);
        }
    }
}