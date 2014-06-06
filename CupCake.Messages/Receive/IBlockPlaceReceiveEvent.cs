using CupCake.Messages.Blocks;

namespace CupCake.Messages.Receive
{
    public interface IBlockPlaceReceiveEvent
    {
        Layer Layer { get; set; }
        int PosX { get; set; }
        int PosY { get; set; }
        Block Block { get; set; }
    }
}