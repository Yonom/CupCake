using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class LabelPlaceReceiveMessage : BlockPlaceReceiveMessage
{

	//2
	public readonly LabelBlock LabelBlock;
	//3

	public readonly string Text;
	internal LabelPlaceReceiveMessage(Message message) : base(message, Layer.Foreground, message.GetInteger(0), message.GetInteger(1), (Block)message.GetInteger(2))
	{

		LabelBlock = (LabelBlock)message.GetInteger(2);
		Text = message.GetString(3);
	}
}
