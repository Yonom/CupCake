using PlayerIOClient;

public sealed class InfoReceiveMessage : ReceiveMessage
{
    //0
    //1

    public readonly string Text;
    public readonly string Title;

    internal InfoReceiveMessage(Message message)
        : base(message)
    {
        this.Title = message.GetString(0);
        this.Text = message.GetString(1);
    }
}