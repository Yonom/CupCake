using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class GodModeReceiveMessage : ReceiveMessage
{

	//0
	public readonly int UserID;
	//1

	public readonly bool IsGod;
	internal GodModeReceiveMessage(Message message) : base(message)
	{

		UserID = message.GetInteger(0);
		IsGod = message.GetBoolean(1);
	}
}
