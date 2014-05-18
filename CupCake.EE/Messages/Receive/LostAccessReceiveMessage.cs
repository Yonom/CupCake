using PlayerIOClient;

public sealed class LostAccessReceiveMessage : ReceiveMessage
{
    //No arguments

    internal LostAccessReceiveMessage(Message message)
        : base(message)
    {
    }
}