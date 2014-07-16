using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class WorldPortalPlaceSendEvent : SendEvent, IBlockPlaceSendEvent
    {
        public WorldPortalPlaceSendEvent(Layer layer, int x, int y, WorldPortalBlock block, string worldPortalTarget)
        {
            Block = block;
            X = x;
            Y = y;
            Layer = BlockUtils.CorrectLayer((Block) block, layer);

            WorldPortalTarget = worldPortalTarget;
        }

        public WorldPortalBlock Block { get; set; }
        public string WorldPortalTarget { get; set; }

        Block IBlockPlaceSendEvent.Block
        {
            get { return (Block) Block; }
            set { Block = (WorldPortalBlock) value; }
        }

        public Layer Layer { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public string Encryption { get; set; }

        public override Message GetMessage()
        {
            return Message.Create(Encryption, (int) Layer, X, Y, (int) Block,
                WorldPortalTarget);
        }
    }
}