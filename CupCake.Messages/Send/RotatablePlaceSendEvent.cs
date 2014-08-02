using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class RotatablePlaceSendEvent : SendEvent, IBlockPlaceSendEvent
    {
        public RotatablePlaceSendEvent(Layer layer, int x, int y, RotatableBlock block, uint rotation)
        {
            this.Block = block;
            this.X = x;
            this.Y = y;
            this.Layer = BlockUtils.CorrectLayer((Block)block, layer);

            this.Rotation = rotation;
        }

        public RotatableBlock Block { get; set; }
        public uint Rotation { get; set; }

        Block IBlockPlaceSendEvent.Block
        {
            get { return (Block)this.Block; }
            set { this.Block = (RotatableBlock)value; }
        }

        public Layer Layer { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public string Encryption { get; set; }

        public override Message GetMessage()
        {
            return Message.Create(this.Encryption, (int)this.Layer, this.X, this.Y, (int)this.Block, this.Rotation);
        }
    }
}