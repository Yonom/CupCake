using PlayerIOClient;

public sealed class SoundPlaceSendMessage : BlockPlaceSendMessage
{
    public readonly int SoundID;

    public SoundPlaceSendMessage(string encryption, Layer layer, int x, int y, SoundBlock block, int soundID)
        : base(encryption, layer, x, y, (Block)block)
    {
        this.SoundID = soundID;
    }

    internal override Message GetMessage()
    {
        if (IsSound(this.Block))
        {
            Message message = base.GetMessage();
            message.Add(this.SoundID);
            return message;
        }
        return base.GetMessage();
    }
}