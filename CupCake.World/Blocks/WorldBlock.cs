using System;
using CupCake.EE.Blocks;
using CupCake.EE.Events.Send;

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


        public static bool operator ==(WorldBlock a, BlockPlaceSendEvent b)
        {
            if (((object)a == null) || (b == null))
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(WorldBlock a, BlockPlaceSendEvent b)
        {
            return !(a == b);
        }

        protected virtual bool Equals(WorldBlock other)
        {
            if ((object)other == null)
            {
                return false;
            }

            return this.Block == other.Block;
        }

        protected virtual bool Equals(BlockPlaceSendEvent other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Block == other.Block;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((WorldBlock)obj);
        }

        public override int GetHashCode()
        {
            return (int)this._block;
        }
    }
}