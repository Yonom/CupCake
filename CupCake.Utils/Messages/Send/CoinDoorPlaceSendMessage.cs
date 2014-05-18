using CupCake.Utils.Blocks;
using PlayerIOClient;

namespace CupCake.Utils.Messages.Send
{
    public sealed class CoinDoorPlaceSendMessage : BlockPlaceSendMessage
    {
        public readonly int CoinsToCollect;

        public CoinDoorPlaceSendMessage(string encryption, Layer layer, int x, int y, CoinDoorBlock block,
            int coinsToCollect)
            : base(encryption, layer, x, y, (Block)block)
        {
            this.CoinsToCollect = coinsToCollect;
        }

        internal override Message GetMessage()
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