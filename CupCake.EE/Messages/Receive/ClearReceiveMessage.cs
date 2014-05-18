using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class ClearReceiveMessage : ReceiveMessage
{

	//0
	public readonly int RoomWidth;
	//1

	public readonly int RoomHeight;
	internal ClearReceiveMessage(Message message) : base(message)
	{

		RoomWidth = message.GetInteger(0);
		RoomHeight = message.GetInteger(1);
	}
}
