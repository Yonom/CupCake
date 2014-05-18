using PlayerIOClient;

public class TouchPlayerSendMessage : SendMessage
{
    public readonly Potion Reason;
    public readonly int UserID;

    public TouchPlayerSendMessage(int userID, Potion reason)
    {
        this.UserID = userID;
        this.Reason = reason;
    }

    internal override Message GetMessage()
    {
        return Message.Create("touch", this.UserID, this.Reason);
    }
}