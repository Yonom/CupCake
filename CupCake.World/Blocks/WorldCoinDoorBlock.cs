using CupCake.EE.Blocks;

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
    }
}