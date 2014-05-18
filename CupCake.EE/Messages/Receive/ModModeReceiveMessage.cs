using PlayerIOClient;

public sealed class ModModeReceiveMessage : ReceiveMessage
{
    //0

    public readonly int UserID;

    internal ModModeReceiveMessage(Message message)
        : base(message)
    {
        this.UserID = message.GetInteger(0);
    }
}