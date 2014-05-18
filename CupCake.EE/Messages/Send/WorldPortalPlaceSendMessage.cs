using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class WorldPortalPlaceSendMessage : BlockPlaceSendMessage
{


	public readonly string WorldPortalTarget;
    public WorldPortalPlaceSendMessage(string encryption, Layer layer, int x, int y, WorldPortalBlock block, string worldPortalTarget)
        : base(encryption, layer, x, y, (Block)block)
	{

		this.WorldPortalTarget = worldPortalTarget;
	}

	internal override Message GetMessage()
	{
		if (IsWorldPortal(Block)) {
			Message message = base.GetMessage();
			message.Add(WorldPortalTarget);
			return message;
		} else {
			return base.GetMessage();
		}
	}
}
