using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class BlockPlaceReceiveEvent : ReceiveEvent, IBlockPlaceReceiveEvent, IUserReceiveEvent
    {
        public BlockPlaceReceiveEvent(Message message)
            : base(message)
        {
            this.Layer = (Layer)message.GetInteger(0);
            this.PosX = message.GetInteger(1);
            this.PosY = message.GetInteger(2);
            this.Block = (Block)message.GetInteger(3);
            this.UserId = message.GetInteger(4);
        }

        public int UserId { get; set; }
        public Block Block { get; set; }
        public Layer Layer { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
    }
}