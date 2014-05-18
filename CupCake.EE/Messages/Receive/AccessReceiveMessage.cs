using PlayerIOClient;

public sealed class AccessReceiveMessage : ReceiveMessage
{
    //No arguments

    internal AccessReceiveMessage(Message message)
        : base(message)
    {
    }
}