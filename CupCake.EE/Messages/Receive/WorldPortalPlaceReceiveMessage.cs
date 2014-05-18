using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class WorldPortalPlaceReceiveMessage : BlockPlaceReceiveMessage
{

	//2
	public readonly WorldPortalBlock WorldPortalBlock;
	//3

	public readonly string WorldPortalTarget;
	internal WorldPortalPlaceReceiveMessage(Message message) : base(message, Layer.Foreground, message.GetInteger(0), message.GetInteger(1), (Block)message.GetInteger(2))
	{

		WorldPortalBlock = (WorldPortalBlock)message.GetInteger(2);
		WorldPortalTarget = message.GetString(3);
	}
}
