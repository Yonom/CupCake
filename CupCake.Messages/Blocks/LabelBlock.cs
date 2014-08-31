using System;

namespace CupCake.Messages.Blocks
{
    public enum LabelBlock
    {
        [Obsolete("Use LabelBlock.DecorLabel instead.")]
        DecorationLabel = Block.DecorLabel,
        [Obsolete("Use LabelBlock.DecorSign instead.")]
        DecorationSign = Block.DecorSign,

        DecorLabel = Block.DecorLabel,
        DecorSign = Block.DecorSign
    }
}