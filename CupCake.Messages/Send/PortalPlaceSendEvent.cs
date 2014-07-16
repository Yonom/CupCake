using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class PortalPlaceSendEvent : SendEvent, IBlockPlaceSendEvent
    {
        public PortalPlaceSendEvent(Layer layer, int x, int y, PortalBlock block, int portalId, int portalTarget,
            PortalRotation portalRotation)
        {
            Block = block;
            X = x;
            Y = y;
            Layer = BlockUtils.CorrectLayer((Block) block, layer);

            PortalId = portalId;
            PortalTarget = portalTarget;
            PortalRotation = portalRotation;
        }

        public PortalBlock Block { get; set; }
        public PortalRotation PortalRotation { get; set; }
        public int PortalId { get; set; }
        public int PortalTarget { get; set; }

        Block IBlockPlaceSendEvent.Block
        {
            get { return (Block) Block; }
            set { Block = (PortalBlock) value; }
        }

        public Layer Layer { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public string Encryption { get; set; }

        public override Message GetMessage()
        {
            return Message.Create(Encryption, (int) Layer, X, Y, (int) Block,
                (int) PortalRotation, PortalId, PortalTarget);
        }
    }
}