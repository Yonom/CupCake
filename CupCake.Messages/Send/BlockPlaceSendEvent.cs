using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class BlockPlaceSendEvent : SendEvent, IBlockPlaceSendEvent
    {
        public BlockPlaceSendEvent(Layer layer, int x, int y, Block block)
        {
            Layer = BlockUtils.CorrectLayer(block, layer);
            X = x;
            Y = y;
            Block = block;
        }

        public Block Block { get; set; }
        public Layer Layer { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public string Encryption { get; set; }

        public override Message GetMessage()
        {
            return Message.Create(Encryption, (int) Layer, X, Y, (int) Block);
        }
    }
}