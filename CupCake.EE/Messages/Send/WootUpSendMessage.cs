using PlayerIOClient;

public class WootUpSendMessage : SendMessage
{
    //No arguments

    internal override Message GetMessage()
    {
        return Message.Create("wootup");
    }
}