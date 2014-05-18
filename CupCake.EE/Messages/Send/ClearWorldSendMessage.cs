using PlayerIOClient;

public sealed class ClearWorldSendMessage : SendMessage
{
    //No arguments

    internal override Message GetMessage()
    {
        return Message.Create("clear");
    }
}