using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class PortalPlaceReceiveMessage : BlockPlaceReceiveMessage
    {
        public PortalPlaceReceiveMessage(Message message)
            : base(message, Layer.Foreground, message.GetInteger(0), message.GetInteger(1), (Block)message.GetInteger(2)
                )
        {
            this.PortalBlock = (PortalBlock)message.GetInteger(2);
            this.PortalRotation = (PortalRotation)message.GetInteger(3);
            this.PortalId = message.GetInteger(4);
            this.PortalTarget = message.GetInteger(5);
        }

        public PortalBlock PortalBlock { get; private set; }
        public int PortalId { get; private set; }
        public PortalRotation PortalRotation { get; private set; }
        public int PortalTarget { get; private set; }
    }
}