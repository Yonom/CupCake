using CupCake.EE.Blocks;
using CupCake.EE.Events.Send;

namespace CupCake.World.Blocks
{
    public class WorldPortalBlock : WorldBlock
    {
        private readonly int _portalId;
        private readonly PortalRotation _portalRotation;
        private readonly int _portalTarget;

        public WorldPortalBlock(PortalBlock block, PortalRotation portalRotation, int portalId, int portalTarget)
            : base((Block)block)
        {
            this._portalRotation = portalRotation;
            this._portalId = portalId;
            this._portalTarget = portalTarget;
        }

        public override BlockType BlockType
        {
            get { return BlockType.Portal; }
        }

        public PortalRotation PortalRotation
        {
            get { return this._portalRotation; }
        }


        public int PortalId
        {
            get { return this._portalId; }
        }


        public int PortalTarget
        {
            get { return this._portalTarget; }
        }

        protected override bool Equals(BlockPlaceSendEvent other)
        {
            var portalEvent = other as PortalPlaceSendEvent;
            if (portalEvent != null)
                return base.Equals(portalEvent) && portalEvent.PortalRotation == this.PortalRotation &&
                       portalEvent.PortalId == this.PortalId && portalEvent.PortalTarget == this.PortalTarget;

            return base.Equals(other);
        }

        protected override bool Equals(WorldBlock other)
        {
            var portalBlock = other as WorldPortalBlock;
            if (portalBlock != null)
                return base.Equals(portalBlock) && portalBlock.PortalRotation == this.PortalRotation &&
                       portalBlock.PortalId == this.PortalId && portalBlock.PortalTarget == this.PortalTarget;

            return base.Equals(other);
        }
    }
}