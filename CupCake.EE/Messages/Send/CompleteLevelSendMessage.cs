using PlayerIOClient;

public sealed class CompleteLevelSendMessage : SendMessage
{
    //No arguments

    internal override Message GetMessage()
    {
        return Message.Create("levelcomplete");
    }
}