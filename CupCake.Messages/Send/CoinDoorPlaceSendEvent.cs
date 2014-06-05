using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class CoinDoorPlaceSendEvent : BlockPlaceSendEvent
    {
        public CoinDoorPlaceSendEvent(Layer layer, int x, int y, CoinDoorBlock block,
            int coinsToCollect)
            : base(layer, x, y, (Block)block)
        {
            this.CoinsToCollect = coinsToCollect;
        }

        public int CoinsToCollect { get; set; }

        public override Message GetMessage()
        {
            if (IsCoinDoor(this.Block))
            {
                Message message = base.GetMessage();
                message.Add(this.CoinsToCollect);
                return message;
            }
            return base.GetMessage();
        }
    }
}