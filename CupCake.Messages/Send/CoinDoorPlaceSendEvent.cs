using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class CoinDoorPlaceSendEvent : SendEvent, IBlockPlaceSendEvent
    {
        public CoinDoorPlaceSendEvent(Layer layer, int x, int y, CoinDoorBlock block, int coinsToCollect)
        {
            Block = block;
            X = x;
            Y = y;
            Layer = BlockUtils.CorrectLayer((Block) block, layer);

            CoinsToCollect = coinsToCollect;
        }

        public CoinDoorBlock Block { get; set; }
        public int CoinsToCollect { get; set; }

        Block IBlockPlaceSendEvent.Block
        {
            get { return (Block) Block; }
            set { Block = (CoinDoorBlock) value; }
        }

        public Layer Layer { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public string Encryption { get; set; }

        public override Message GetMessage()
        {
            return Message.Create(Encryption, (int) Layer, X, Y, (int) Block, CoinsToCollect);
        }
    }
}