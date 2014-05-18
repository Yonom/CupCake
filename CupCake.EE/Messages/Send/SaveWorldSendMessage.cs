using PlayerIOClient;

public sealed class SaveWorldSendMessage : SendMessage
{
    //No arguments

    internal override Message GetMessage()
    {
        return Message.Create("save");
    }
}