using System;
using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public class PortalPlaceSendEvent : BlockPlaceSendEvent
    {
        public PortalPlaceSendEvent(Layer layer, int x, int y, PortalBlock block, int portalId,
            int portalTarget, PortalRotation portalRotation)
            : base(layer, x, y, (Block)block)
        {
            this.PortalId = portalId;
            this.PortalTarget = portalTarget;
            this.PortalRotation = portalRotation;
        }

        public int PortalId { get; set; }
        public PortalRotation PortalRotation { get; set; }
        public int PortalTarget { get; set; }

        public override Message GetMessage()
        {
            if (IsPortal(this.Block))
            {
                Message message = base.GetMessage();
                message.Add(Convert.ToInt32(this.PortalRotation), this.PortalId, this.PortalTarget);
                return message;
            }
            return base.GetMessage();
        }
    }
}