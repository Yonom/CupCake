using CupCake.EE.Blocks;

namespace CupCake.World.Blocks
{
    public class WorldRotatableBlock : WorldBlock
    {
        private readonly int _rotation;

        public WorldRotatableBlock(RotatableBlock block, int coinsToCollect)
            : base((Block)block)
        {
            this._rotation = coinsToCollect;
        }

        public override BlockType BlockType
        {
            get { return BlockType.Rotatable; }
        }

        public int Rotation
        {
            get { return this._rotation; }
        }
    }
}