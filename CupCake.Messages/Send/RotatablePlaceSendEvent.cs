using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class RotatablePlaceSendEvent : SendEvent, IBlockPlaceSendEvent
    {
        public RotatablePlaceSendEvent(Layer layer, int x, int y, RotatableBlock block, int rotation)
        {
            Block = block;
            X = x;
            Y = y;
            Layer = BlockUtils.CorrectLayer((Block) block, layer);

            Rotation = rotation;
        }

        public RotatableBlock Block { get; set; }
        public int Rotation { get; set; }

        Block IBlockPlaceSendEvent.Block
        {
            get { return (Block) Block; }
            set { Block = (RotatableBlock) value; }
        }

        public Layer Layer { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public string Encryption { get; set; }

        public override Message GetMessage()
        {
            return Message.Create(Encryption, (int) Layer, X, Y, (int) Block, Rotation);
        }
    }
}