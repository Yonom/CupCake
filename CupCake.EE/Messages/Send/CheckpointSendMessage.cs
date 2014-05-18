using PlayerIOClient;

public class CheckpointSendMessage : SendMessage
{
    public readonly int X;

    public readonly int Y;

    public CheckpointSendMessage(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    internal override Message GetMessage()
    {
        return Message.Create("checkpoint", this.X, this.Y);
    }
}