using PlayerIOClient;

public class DeathSendMessage : SendMessage
{
    //No arguments

    internal override Message GetMessage()
    {
        return Message.Create("death");
    }
}