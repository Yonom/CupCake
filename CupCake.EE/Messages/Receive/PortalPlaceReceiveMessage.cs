using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class PortalPlaceReceiveMessage : BlockPlaceReceiveMessage
{

	//2
	public readonly PortalBlock PortalBlock;
	//3
	public readonly PortalRotation PortalRotation;
	//4
	public readonly int PortalID;
	//5

	public readonly int PortalTarget;
	internal PortalPlaceReceiveMessage(Message message) : base(message, Layer.Foreground, message.GetInteger(0), message.GetInteger(1), (Block)message.GetInteger(2))
	{

		PortalBlock = (PortalBlock)message.GetInteger(2);
		PortalRotation = (PortalRotation)message.GetInteger(3);
		PortalID = message.GetInteger(4);
		PortalTarget = message.GetInteger(5);
	}
}
