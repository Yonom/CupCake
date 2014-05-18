using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class MoveReceiveMessage : ReceiveMessage
{

	//0
	public readonly int UserID;
	//1
	public readonly int PlayerPosX;
	//2
	public readonly int PlayerPosY;
	//3
	public readonly double SpeedX;
	//4
	public readonly double SpeedY;
	//5
	public readonly double ModifierX;
	//6
	public readonly double ModifierY;
	//7
	public readonly double Horizontal;
	//8
	public readonly double Vertical;
	//9
	public readonly int Coins;
	//10

	public readonly bool IsPurple;
	internal MoveReceiveMessage(Message message) : base(message)
	{

		UserID = message.GetInteger(0);
		PlayerPosX = message.GetInteger(1);
		PlayerPosY = message.GetInteger(2);
		SpeedX = message.GetDouble(3);
		SpeedY = message.GetDouble(4);
		ModifierX = message.GetDouble(5);
		ModifierY = message.GetDouble(6);
		Horizontal = message.GetDouble(7);
		Vertical = message.GetDouble(8);
		Coins = message.GetInteger(9);
		IsPurple = message.GetBoolean(10);
	}

	public int BlockX {
		get { return PlayerPosX + 8 >> 4; }
	}

	public int BlockY {
		get { return PlayerPosY + 8 >> 4; }
	}
}
