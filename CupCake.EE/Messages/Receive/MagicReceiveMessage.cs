using PlayerIOClient;

public class MagicReceiveMessage : ReceiveMessage
{
    //0

    public readonly int UserID;

    internal MagicReceiveMessage(Message message)
        : base(message)
    {
        this.UserID = message.GetInteger(0);
    }
}