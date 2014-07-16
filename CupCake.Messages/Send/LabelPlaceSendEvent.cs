using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class LabelPlaceSendEvent : SendEvent, IBlockPlaceSendEvent
    {
        public LabelPlaceSendEvent(Layer layer, int x, int y, LabelBlock block, string text)
        {
            Block = block;
            X = x;
            Y = y;
            Layer = BlockUtils.CorrectLayer((Block) block, layer);

            Text = text;
        }

        public LabelBlock Block { get; set; }
        public string Text { get; set; }

        Block IBlockPlaceSendEvent.Block
        {
            get { return (Block) Block; }
            set { Block = (LabelBlock) value; }
        }

        public Layer Layer { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public string Encryption { get; set; }

        public override Message GetMessage()
        {
            return Message.Create(Encryption, (int) Layer, X, Y, (int) Block, Text);
        }
    }
}