using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class CoinSendMessage : SendMessage
{

	public readonly int Coins;
	public readonly int CoinX;

	public readonly int CoinY;
	public CoinSendMessage(int coins, int coinX, int coinY)
	{
		this.Coins = coins;
		this.CoinX = coinX;
		this.CoinY = coinY;
	}

	internal override Message GetMessage()
	{
		return Message.Create("c", Coins, CoinX, CoinY);
	}
}
