using CupCake.EE;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class TeleportEveryoneReceiveMessage : ReceiveMessage
{

	//0

	public readonly bool ResetCoins;

	public readonly Dictionary<int, Point> Coordinates = new Dictionary<int, Point>();
	internal TeleportEveryoneReceiveMessage(Message message) : base(message)
	{

		ResetCoins = message.GetBoolean(0);

		for (uint i = 1; i <= message.Count - 1u; i += 3) {
			Coordinates.Add(message.GetInteger(i), new Point(message.GetInteger(i + 1u), message.GetInteger(i + 2u)));
		}
	}
}
