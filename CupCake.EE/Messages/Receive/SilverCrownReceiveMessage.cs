using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class SilverCrownReceiveMessage : ReceiveMessage
{

	//0

	public readonly int UserID;
	internal SilverCrownReceiveMessage(Message message) : base(message)
	{

		UserID = message.GetInteger(0);
	}
}
