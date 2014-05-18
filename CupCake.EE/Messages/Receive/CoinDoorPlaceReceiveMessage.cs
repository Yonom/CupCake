using PlayerIOClient;

public sealed class CoinDoorPlaceReceiveMessage : BlockPlaceReceiveMessage
{
    //2
    public readonly CoinDoorBlock CoinDoorBlock;
    //3

    public readonly int CoinsToOpen;

    internal CoinDoorPlaceReceiveMessage(Message message)
        : base(message, Layer.Foreground, message.GetInteger(0), message.GetInteger(1), (Block)message.GetInteger(2))
    {
        this.CoinDoorBlock = (CoinDoorBlock)message.GetInteger(2);
        this.CoinsToOpen = message.GetInteger(3);
    }
}