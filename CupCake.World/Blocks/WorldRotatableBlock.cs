using CupCake.EE.Blocks;
using CupCake.EE.Events.Send;

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

        protected override bool Equals(BlockPlaceSendEvent other)
        {
            var rotatableEvent = other as RotatablePlaceSendEvent;
            if (rotatableEvent != null)
                return base.Equals(rotatableEvent) && rotatableEvent.Rotation == this.Rotation;

            return base.Equals(other);
        }

        protected override bool Equals(WorldBlock other)
        {
            var rotatableBlock = other as WorldRotatableBlock;
            if (rotatableBlock != null)
                return base.Equals(rotatableBlock) && rotatableBlock.Rotation == this.Rotation;

            return base.Equals(other);
        }
    }
}