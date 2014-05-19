using CupCake.EE.Blocks;

namespace CupCake.World.Blocks
{
    public class WorldWorldPortalBlock : WorldBlock
    {
        private readonly string _portalTarget;

        public WorldWorldPortalBlock(EE.Blocks.WorldPortalBlock block, string portalTarget)
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
    }
}