using System;
using PlayerIOClient;

public sealed class PortalPlaceSendMessage : BlockPlaceSendMessage
{
    public readonly int PortalID;

    public readonly PortalRotation PortalRotation;
    public readonly int PortalTarget;

    public PortalPlaceSendMessage(string encryption, Layer layer, int x, int y, PortalBlock block, int portalID,
        int portalTarget, PortalRotation portalRotation)
        : base(encryption, layer, x, y, (Block)block)
    {
        this.PortalID = portalID;
        this.PortalTarget = portalTarget;
        this.PortalRotation = portalRotation;
    }

    internal override Message GetMessage()
    {
        if (IsPortal(this.Block))
        {
            Message message = base.GetMessage();
            message.Add(Convert.ToInt32(this.PortalRotation), this.PortalID, this.PortalTarget);
            return message;
        }
        return base.GetMessage();
    }
}