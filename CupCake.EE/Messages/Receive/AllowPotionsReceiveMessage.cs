using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public class AllowPotionsReceiveMessage : ReceiveMessage
{

	//0

	public readonly bool Allowed;
	internal AllowPotionsReceiveMessage(Message message) : base(message)
	{

		Allowed = message.GetBoolean(0);
	}
}
