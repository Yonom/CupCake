using PlayerIOClient;

public sealed class ChangeWorldEditKeySendMessage : SendMessage
{
    public readonly string EditKey;

    public ChangeWorldEditKeySendMessage(string editKey)
    {
        this.EditKey = editKey;
    }

    internal override Message GetMessage()
    {
        return Message.Create("key", this.EditKey);
    }
}