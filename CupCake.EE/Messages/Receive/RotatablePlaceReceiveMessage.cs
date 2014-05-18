using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public class RotatablePlaceReceiveMessage : BlockPlaceReceiveMessage
{

	//2
	public readonly RotatableBlock RotatableBlock;
	//3

	public readonly int Rotation;
	internal RotatablePlaceReceiveMessage(Message message) : base(message, Layer.Foreground, message.GetInteger(0), message.GetInteger(1), (Block)message.GetInteger(2))
	{

		RotatableBlock = (RotatableBlock)message.GetInteger(2);
		Rotation = message.GetInteger(3);
	}
}
