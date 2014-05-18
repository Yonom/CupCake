using PlayerIOClient;

public sealed class CoinSendMessage : SendMessage
{
    public readonly int CoinX;

    public readonly int CoinY;
    public readonly int Coins;

    public CoinSendMessage(int coins, int coinX, int coinY)
    {
        this.Coins = coins;
        this.CoinX = coinX;
        this.CoinY = coinY;
    }

    internal override Message GetMessage()
    {
        return Message.Create("c", this.Coins, this.CoinX, this.CoinY);
    }
}