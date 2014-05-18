using PlayerIOClient;

public sealed class PressRedKeySendMessage : SendMessage
{
    public PressRedKeySendMessage(string encryption)
    {
        this.Encryption = encryption;
    }

    public string Encryption { get; set; }

    internal override Message GetMessage()
    {
        return Message.Create(this.Encryption + "r");
    }
}