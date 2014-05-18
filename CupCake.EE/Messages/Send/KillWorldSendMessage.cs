using PlayerIOClient;

public sealed class KillWorldSendMessage : SendMessage
{
    //No arguments

    internal override Message GetMessage()
    {
        return Message.Create("kill");
    }
}