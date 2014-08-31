using System;

namespace CupCake.Messages.Blocks
{
    public enum PortalBlock
    {
        [Obsolete("Use PortalBlock.Portal instead.")]
        BlockPortal = Block.Portal,
        [Obsolete("Use PortalBlock.InvisiblePortal instead.")]
        BlockInvisiblePortal = Block.InvisiblePortal,

        Portal = Block.Portal,
        InvisiblePortal = Block.InvisiblePortal
    }
}