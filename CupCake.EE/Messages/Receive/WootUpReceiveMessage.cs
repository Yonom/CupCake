using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public class WootUpReceiveMessage : ReceiveMessage
{

	//0

	public readonly int UserID;
	internal WootUpReceiveMessage(Message message) : base(message)
	{

		UserID = message.GetInteger(0);
	}
}
