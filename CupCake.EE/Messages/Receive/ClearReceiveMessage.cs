using PlayerIOClient;

public sealed class ClearReceiveMessage : ReceiveMessage
{
    //0
    //1

    public readonly int RoomHeight;
    public readonly int RoomWidth;

    internal ClearReceiveMessage(Message message)
        : base(message)
    {
        this.RoomWidth = message.GetInteger(0);
        this.RoomHeight = message.GetInteger(1);
    }
}