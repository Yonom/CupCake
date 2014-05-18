using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public class LevelUpReceiveMessage : ReceiveMessage
{

	//0
	public readonly int UserID;
	//1

	public readonly int NewClass;
	internal LevelUpReceiveMessage(Message message) : base(message)
	{
		UserID = message.GetInteger(0);
		NewClass = message.GetInteger(1);
	}
}
