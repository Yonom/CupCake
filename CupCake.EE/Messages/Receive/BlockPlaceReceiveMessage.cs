using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public class BlockPlaceReceiveMessage : ReceiveMessage
    {
        public readonly Block Block;
        public readonly Layer Layer;
        public readonly int PosX;
        public readonly int PosY;

        public BlockPlaceReceiveMessage(Message message)
            : base(message)
        {
            this.Layer = (Layer)message.GetInteger(0);
            this.PosX = message.GetInteger(1);
            this.PosY = message.GetInteger(2);
            this.Block = (Block)message.GetInteger(3);
        }

        protected BlockPlaceReceiveMessage(Message message, Layer layer, int posX, int posY, Block block)
            : base(message)
        {
            this.Layer = layer;
            this.PosX = posX;
            this.PosY = posY;
            this.Block = block;
        }
    }
}