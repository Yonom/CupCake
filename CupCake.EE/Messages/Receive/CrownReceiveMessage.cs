using PlayerIOClient;

public sealed class CrownReceiveMessage : ReceiveMessage
{
    //0

    public readonly int UserID;

    internal CrownReceiveMessage(Message message)
        : base(message)
    {
        this.UserID = message.GetInteger(0);
    }
}