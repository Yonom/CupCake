using CupCake.EE.Blocks;
using CupCake.EE.Events.Send;

namespace CupCake.World.Blocks
{
    public class WorldLabelBlock : WorldBlock
    {
        private readonly string _text;

        public WorldLabelBlock(LabelBlock block, string text)
            : base((Block)block)
        {
            this._text = text;
        }

        public override BlockType BlockType
        {
            get { return BlockType.Label; }
        }

        public string Text
        {
            get { return this._text; }
        }

        protected override bool Equals(BlockPlaceSendEvent other)
        {
            var labelEvent = other as LabelPlaceSendEvent;
            if (labelEvent != null)
                return base.Equals(labelEvent) && labelEvent.Text == this.Text;

            return base.Equals(other);
        }

        protected override bool Equals(WorldBlock other)
        {
            var labelBlock = other as WorldLabelBlock;
            if (labelBlock != null)
                return base.Equals(labelBlock) && labelBlock.Text == this.Text;

            return base.Equals(other);
        }
    }
}