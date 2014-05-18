using PlayerIOClient;

public sealed class AutoSaySendMessage : SendMessage
{
    public readonly AutoText Text;

    public AutoSaySendMessage(AutoText text)
    {
        this.Text = text;
    }

    internal override Message GetMessage()
    {
        return Message.Create("autosay", this.Text);
    }
}