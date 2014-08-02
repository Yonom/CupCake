using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class CoinDoorPlaceSendEvent : SendEvent, IBlockPlaceSendEvent
    {
        public CoinDoorPlaceSendEvent(Layer layer, int x, int y, CoinDoorBlock block, uint coinsToCollect)
        {
            this.Block = block;
            this.X = x;
            this.Y = y;
            this.Layer = BlockUtils.CorrectLayer((Block)block, layer);

            this.CoinsToCollect = coinsToCollect;
        }

        public CoinDoorBlock Block { get; set; }
        public uint CoinsToCollect { get; set; }

        Block IBlockPlaceSendEvent.Block
        {
            get { return (Block)this.Block; }
            set { this.Block = (CoinDoorBlock)value; }
        }

        public Layer Layer { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public string Encryption { get; set; }

        public override Message GetMessage()
        {
            return Message.Create(this.Encryption, (int)this.Layer, this.X, this.Y, (int)this.Block, this.CoinsToCollect);
        }
    }
}