using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class WorldPortalPlaceSendEvent : SendEvent, IBlockPlaceSendEvent
    {
        public WorldPortalPlaceSendEvent(Layer layer, int x, int y, WorldPortalBlock block, string worldPortalTarget)
        {
            this.Block = block;
            this.X = x;
            this.Y = y;
            this.Layer = BlockHelper.CorrectLayer((Block)block, layer);

            this.WorldPortalTarget = worldPortalTarget;
        }

        Block IBlockPlaceSendEvent.Block
        {
            get { return (Block)this.Block; }
            set { this.Block = (WorldPortalBlock)value; }
        }
        public WorldPortalBlock Block { get; set; }
        public Layer Layer { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public string Encryption { get; set; }

        public string WorldPortalTarget { get; set; }

        public override Message GetMessage()
        {
            return Message.Create(this.Encryption, (int)this.Layer, this.X, this.Y, (int)this.Block, this.WorldPortalTarget);
        }
    }
}