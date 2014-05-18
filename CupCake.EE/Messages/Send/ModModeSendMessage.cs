using PlayerIOClient;

public sealed class ModModeSendMessage : SendMessage
{
    //No arguments

    internal override Message GetMessage()
    {
        return Message.Create("mod");
    }
}