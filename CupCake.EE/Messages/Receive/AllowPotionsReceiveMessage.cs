using PlayerIOClient;

public class AllowPotionsReceiveMessage : ReceiveMessage
{
    //0

    public readonly bool Allowed;

    internal AllowPotionsReceiveMessage(Message message)
        : base(message)
    {
        this.Allowed = message.GetBoolean(0);
    }
}