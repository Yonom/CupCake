using PlayerIOClient;

public sealed class CoinReceiveMessage : ReceiveMessage
{
    //0
    //1

    public readonly int Coins;
    public readonly int UserID;

    internal CoinReceiveMessage(Message message)
        : base(message)
    {
        this.UserID = message.GetInteger(0);
        this.Coins = message.GetInteger(1);
    }
}