using PlayerIOClient;

public sealed class SaveDoneReceiveMessage : ReceiveMessage
{
    //No arguments

    internal SaveDoneReceiveMessage(Message message)
        : base(message)
    {
    }
}