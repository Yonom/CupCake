using CupCake.EE.Blocks;

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
    }
}