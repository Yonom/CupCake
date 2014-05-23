using CupCake.EE.Blocks;
using CupCake.EE.Events.Send;

namespace CupCake.World.Blocks
{
    public class WorldCoinDoorBlock : WorldBlock
    {
        private readonly int _coinsToCollect;

        public WorldCoinDoorBlock(CoinDoorBlock block, int coinsToCollect)
            : base((Block)block)
        {
            this._coinsToCollect = coinsToCollect;
        }

        public override BlockType BlockType
        {
            get { return BlockType.CoinDoor; }
        }

        public int CoinsToCollect
        {
            get { return this._coinsToCollect; }
        }

        protected override bool Equals(BlockPlaceSendEvent other)
        {
            var coinEvent = other as CoinDoorPlaceSendEvent;
            if (coinEvent != null)
                return base.Equals(coinEvent) && coinEvent.CoinsToCollect == this.CoinsToCollect;

            return base.Equals(other);
        }

        protected override bool Equals(WorldBlock other)
        {
            var coinBlock = other as WorldCoinDoorBlock;
            if (coinBlock != null)
                return base.Equals(coinBlock) && coinBlock.CoinsToCollect == this.CoinsToCollect;

            return base.Equals(other);
        }
    }
}