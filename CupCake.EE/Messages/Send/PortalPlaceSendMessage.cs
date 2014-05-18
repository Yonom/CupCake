using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class PortalPlaceSendMessage : BlockPlaceSendMessage
{

	public readonly int PortalID;
	public readonly int PortalTarget;

	public readonly PortalRotation PortalRotation;
    public PortalPlaceSendMessage(string encryption, Layer layer, int x, int y, PortalBlock block, int portalID, int portalTarget, PortalRotation portalRotation)
        : base(encryption, layer, x, y, (Block)block)
	{

		this.PortalID = portalID;
		this.PortalTarget = portalTarget;
		this.PortalRotation = portalRotation;
	}

	internal override Message GetMessage()
	{
		if (IsPortal(Block)) {
			Message message = base.GetMessage();
			message.Add(Convert.ToInt32(PortalRotation), PortalID, PortalTarget);
			return message;
		} else {
			return base.GetMessage();
		}
	}
}
