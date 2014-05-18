using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class InitReceiveMessage : ReceiveMessage
{

	//0
	public readonly string OwnerUsername;
	//1
	public readonly string WorldName;
	//2
	public readonly int Plays;
	//3
	public readonly int CurrentWoots;
	//4
	public readonly int TotalWoots;
	//5
	public readonly string Encryption;
	//6
	public readonly int UserID;
	//7
	public readonly int SpawnX;
	//8
	public readonly int SpawnY;
	//9
	public readonly string Username;
	//10
	public readonly bool CanEdit;
	//11
	public readonly bool IsOwner;
	//12
	public readonly int SizeX;
	//13
	public readonly int SizeY;
	//14
	public readonly bool IsTutorialRoom;
	//15
	public readonly double Gravity;
	//16

	public readonly bool AllowPotions;
	internal InitReceiveMessage(Message message) : base(message)
	{

		OwnerUsername = message.GetString(0);
		WorldName = message.GetString(1);
		Plays = message.GetInteger(2);
		CurrentWoots = message.GetInteger(3);
		TotalWoots = message.GetInteger(4);
		Encryption = message.GetString(5);
		UserID = message.GetInteger(6);
		SpawnX = message.GetInteger(7);
		SpawnY = message.GetInteger(8);
		Username = message.GetString(9);
		CanEdit = message.GetBoolean(10);
		IsOwner = message.GetBoolean(11);
		SizeX = message.GetInteger(12);
		SizeY = message.GetInteger(13);
		IsTutorialRoom = message.GetBoolean(14);
		Gravity = message.GetDouble(15);
		AllowPotions = message.GetBoolean(16);
	}
}
