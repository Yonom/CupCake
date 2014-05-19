using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class PortalPlaceReceiveMessage : BlockPlaceReceiveMessage
    {
        //2
        public readonly PortalBlock PortalBlock;
        //3
        //4
        public readonly int PortalId;
        public readonly PortalRotation PortalRotation;
        //5

        public readonly int PortalTarget;

        public PortalPlaceReceiveMessage(Message message)
            : base(message, Layer.Foreground, message.GetInteger(0), message.GetInteger(1), (Block)message.GetInteger(2)
                )
        {
            this.PortalBlock = (PortalBlock)message.GetInteger(2);
            this.PortalRotation = (PortalRotation)message.GetInteger(3);
            this.PortalId = message.GetInteger(4);
            this.PortalTarget = message.GetInteger(5);
        }
    }
}