using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class CoinReceiveMessage : ReceiveMessage
{

	//0
	public readonly int UserID;
	//1

	public readonly int Coins;
	internal CoinReceiveMessage(Message message) : base(message)
	{

		UserID = message.GetInteger(0);
		Coins = message.GetInteger(1);
	}
}
