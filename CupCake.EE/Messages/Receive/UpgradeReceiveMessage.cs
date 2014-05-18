using PlayerIOClient;

public sealed class UpgradeReceiveMessage : ReceiveMessage
{
    //No arguments

    internal UpgradeReceiveMessage(Message message)
        : base(message)
    {
    }
}