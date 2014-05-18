using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class PotionReceiveMessage : ReceiveMessage
{

	//0
	public readonly int UserID;
	//1
	public readonly Potion Potion;
	//2
	public readonly bool Enabled;
	//3

	public readonly int Timeout;
	internal PotionReceiveMessage(Message message) : base(message)
	{

		UserID = message.GetInteger(0);
		Potion = (Potion)message.GetInteger(1);
		Enabled = message.GetBoolean(2);
		Timeout = message.GetInteger(3);
	}
}
