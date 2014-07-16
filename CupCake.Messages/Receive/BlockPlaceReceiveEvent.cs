using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class BlockPlaceReceiveEvent : ReceiveEvent, IBlockPlaceReceiveEvent, IUserReceiveEvent
    {
        public BlockPlaceReceiveEvent(Message message)
            : base(message)
        {
            Layer = (Layer) message.GetInteger(0);
            PosX = message.GetInteger(1);
            PosY = message.GetInteger(2);
            Block = (Block) message.GetInteger(3);

            if (message.Count >= 5)
            {
                UserId = message.GetInteger(4);
            }
            else
            {
                UserId = -1;
            }
        }

        public Block Block { get; set; }
        public Layer Layer { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int UserId { get; set; }
    }
}