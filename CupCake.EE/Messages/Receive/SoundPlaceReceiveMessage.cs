using PlayerIOClient;

public sealed class SoundPlaceReceiveMessage : BlockPlaceReceiveMessage
{
    //2
    public readonly SoundBlock SoundBlock;
    //3

    public readonly int SoundID;

    internal SoundPlaceReceiveMessage(Message message)
        : base(message, Layer.Foreground, message.GetInteger(0), message.GetInteger(1), (Block)message.GetInteger(2))
    {
        this.SoundBlock = (SoundBlock)message.GetInteger(2);
        this.SoundID = message.GetInteger(3);
    }
}