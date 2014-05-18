using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class SoundPlaceReceiveMessage : BlockPlaceReceiveMessage
{

	//2
	public readonly SoundBlock SoundBlock;
	//3

	public readonly int SoundID;
	internal SoundPlaceReceiveMessage(Message message) : base(message, Layer.Foreground, message.GetInteger(0), message.GetInteger(1), (Block)message.GetInteger(2))
	{

		SoundBlock = (SoundBlock)message.GetInteger(2);
		SoundID = message.GetInteger(3);
	}
}
