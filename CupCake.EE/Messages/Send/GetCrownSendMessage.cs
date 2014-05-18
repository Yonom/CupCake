using PlayerIOClient;

public sealed class GetCrownSendMessage : SendMessage
{
    public GetCrownSendMessage(string encryption)
    {
        this.Encryption = encryption;
    }

    public string Encryption { get; set; }

    internal override Message GetMessage()
    {
        return Message.Create(this.Encryption + "k");
    }
}