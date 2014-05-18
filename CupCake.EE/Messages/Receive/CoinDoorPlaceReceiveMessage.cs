using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class CoinDoorPlaceReceiveMessage : BlockPlaceReceiveMessage
{

	//2
	public readonly CoinDoorBlock CoinDoorBlock;
	//3

	public readonly int CoinsToOpen;
	internal CoinDoorPlaceReceiveMessage(Message message) : base(message, Layer.Foreground, message.GetInteger(0), message.GetInteger(1), (Block)message.GetInteger(2))
	{

		CoinDoorBlock = (CoinDoorBlock)message.GetInteger(2);
		CoinsToOpen = message.GetInteger(3);
	}
}
