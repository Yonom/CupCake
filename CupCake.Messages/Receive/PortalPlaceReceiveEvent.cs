using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class PortalPlaceReceiveEvent : BlockPlaceReceiveEvent
    {
        public PortalPlaceReceiveEvent(Message message)
            : base(message, Layer.Foreground, message.GetInteger(0), message.GetInteger(1), (Block)message.GetInteger(2)
                )
        {
            this.PortalBlock = (PortalBlock)message.GetInteger(2);
            this.PortalRotation = (PortalRotation)message.GetInteger(3);
            this.PortalId = message.GetInteger(4);
            this.PortalTarget = message.GetInteger(5);
        }

        public PortalBlock PortalBlock { get; set; }
        public int PortalId { get; set; }
        public PortalRotation PortalRotation { get; set; }
        public int PortalTarget { get; set; }
    }
}