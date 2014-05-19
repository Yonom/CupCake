using CupCake.EE.Blocks;

namespace CupCake.World.Blocks
{
    public class WorldBlock
    {
        private readonly Block _block;

        public WorldBlock(Block block)
        {
            this._block = block;
        }

        public virtual BlockType BlockType
        {
            get { return BlockType.Normal; }
        }

        public Block Block
        {
            get { return this._block; }
        }
    }
}