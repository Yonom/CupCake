using PlayerIOClient;

public sealed class ResetReceiveMessage : ReceiveMessage
{
    //No arguments

    internal ResetReceiveMessage(Message message)
        : base(message)
    {
    }
}