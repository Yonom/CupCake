using CupCake.Messages.Blocks;

namespace CupCake.Messages.Send
{
    public interface IBlockPlaceSendEvent : IEncryptedSendEvent
    {
        Layer Layer { get; set; }
        int X { get; set; }
        int Y { get; set; }
        Block Block { get; set; }
    }
}