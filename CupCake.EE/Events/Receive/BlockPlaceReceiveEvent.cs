using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public class BlockPlaceReceiveEvent : ReceiveEvent
    {
        public BlockPlaceReceiveEvent(Message message)
            : base(message)
        {
            this.Layer = (Layer)message.GetInteger(0);
            this.PosX = message.GetInteger(1);
            this.PosY = message.GetInteger(2);
            this.Block = (Block)message.GetInteger(3);
        }

        protected BlockPlaceReceiveEvent(Message message, Layer layer, int posX, int posY, Block block)
            : base(message)
        {
            this.Layer = layer;
            this.PosX = posX;
            this.PosY = posY;
            this.Block = block;
        }

        public Block Block { get; private set; }
        public Layer Layer { get; private set; }
        public int PosX { get; private set; }
        public int PosY { get; private set; }
    }
}