using PlayerIOClient;

public sealed class LeftReceiveMessage : ReceiveMessage
{
    //0

    public readonly int UserID;

    internal LeftReceiveMessage(Message message)
        : base(message)
    {
        this.UserID = message.GetInteger(0);
    }
}