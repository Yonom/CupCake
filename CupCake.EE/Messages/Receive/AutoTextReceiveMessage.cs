using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class AutoTextReceiveMessage : ReceiveMessage
{

	//0
	public readonly int UserID;
	//1

	public readonly string AutoText;
	internal AutoTextReceiveMessage(Message message) : base(message)
	{

		UserID = message.GetInteger(0);
		AutoText = message.GetString(1);
	}
}
