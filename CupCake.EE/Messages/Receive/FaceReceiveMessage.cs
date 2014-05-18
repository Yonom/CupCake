using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class FaceReceiveMessage : ReceiveMessage
{

	//0
	public readonly int UserID;
	//1

	public readonly Smiley Face;
	internal FaceReceiveMessage(Message message) : base(message)
	{

		UserID = message.GetInteger(0);
		Face = (Smiley)message.GetInteger(1);
	}
}
