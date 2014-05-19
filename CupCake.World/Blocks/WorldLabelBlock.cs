using CupCake.EE.Blocks;

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
    }
}