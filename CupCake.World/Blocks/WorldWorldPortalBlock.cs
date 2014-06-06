using CupCake.Messages.Blocks;
using CupCake.Messages.Send;

namespace CupCake.World.Blocks
{
    public class WorldWorldPortalBlock : WorldBlock
    {
        private readonly string _portalTarget;

        public WorldWorldPortalBlock(Messages.Blocks.WorldPortalBlock block, string portalTarget)
            : base((Block)block)
        {
            this._portalTarget = portalTarget;
        }

        public override BlockType BlockType
        {
            get { return BlockType.WorldPortal; }
        }

        public string PortalTarget
        {
            get { return this._portalTarget; }
        }

        protected override bool Equals(IBlockPlaceSendEvent other)
        {
            var worldPortalEvent = other as WorldPortalPlaceSendEvent;
            if (worldPortalEvent != null)
                return base.Equals(worldPortalEvent) && worldPortalEvent.WorldPortalTarget == this.PortalTarget;

            return base.Equals(other);
        }

        protected override bool Equals(WorldBlock other)
        {
            var worldPortalBlock = other as WorldWorldPortalBlock;
            if (worldPortalBlock != null)
                return base.Equals(worldPortalBlock) && worldPortalBlock.PortalTarget == this.PortalTarget;

            return base.Equals(other);
        }
    }
}