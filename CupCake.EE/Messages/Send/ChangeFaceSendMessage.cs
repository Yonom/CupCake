using PlayerIOClient;

public sealed class ChangeFaceSendMessage : SendMessage
{
    public readonly Smiley Face;

    public ChangeFaceSendMessage(string encryption, Smiley face)
    {
        this.Encryption = encryption;
        this.Face = face;
    }

    public string Encryption { get; set; }

    internal override Message GetMessage()
    {
        return Message.Create(this.Encryption + "f", this.Face);
    }
}