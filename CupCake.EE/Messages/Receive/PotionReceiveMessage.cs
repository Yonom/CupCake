using PlayerIOClient;

public sealed class PotionReceiveMessage : ReceiveMessage
{
    //0
    //2
    public readonly bool Enabled;
    public readonly Potion Potion;
    //3

    public readonly int Timeout;
    public readonly int UserID;

    internal PotionReceiveMessage(Message message)
        : base(message)
    {
        this.UserID = message.GetInteger(0);
        this.Potion = (Potion)message.GetInteger(1);
        this.Enabled = message.GetBoolean(2);
        this.Timeout = message.GetInteger(3);
    }
}