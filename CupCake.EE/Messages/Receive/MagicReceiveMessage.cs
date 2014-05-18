using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public class MagicReceiveMessage : ReceiveMessage
{

	//0

	public readonly int UserID;
	internal MagicReceiveMessage(Message message) : base(message)
	{
		UserID = message.GetInteger(0);
	}
}
