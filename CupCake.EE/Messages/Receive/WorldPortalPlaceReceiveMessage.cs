using PlayerIOClient;

public sealed class WorldPortalPlaceReceiveMessage : BlockPlaceReceiveMessage
{
    //2
    public readonly WorldPortalBlock WorldPortalBlock;
    //3

    public readonly string WorldPortalTarget;

    internal WorldPortalPlaceReceiveMessage(Message message)
        : base(message, Layer.Foreground, message.GetInteger(0), message.GetInteger(1), (Block)message.GetInteger(2))
    {
        this.WorldPortalBlock = (WorldPortalBlock)message.GetInteger(2);
        this.WorldPortalTarget = message.GetString(3);
    }
}