using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class CoinDoorPlaceSendMessage : BlockPlaceSendMessage
{


	public readonly int CoinsToCollect;
    public CoinDoorPlaceSendMessage(string encryption, Layer layer, int x, int y, CoinDoorBlock block, int coinsToCollect)
        : base(encryption, layer, x, y, (Block)block)
	{

		this.CoinsToCollect = coinsToCollect;
	}

	internal override Message GetMessage()
	{
		if (IsCoinDoor(Block)) {
			Message message = base.GetMessage();
			message.Add(CoinsToCollect);
			return message;
		} else {
			return base.GetMessage();
		}
	}
}
