using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class AddReceiveMessage : ReceiveMessage
{

	//0
	public readonly int UserID;
	//1
	public readonly string Username;
	//2
	public readonly Smiley Face;
	//3
	public readonly int PlayerPosX;
	//4
	public readonly int PlayerPosY;
	//5
	public readonly bool IsGod;
	//6
	public readonly bool IsMod;
	//7
	public readonly bool HasChat;
	//8
	public readonly int Coins;
	//9
	public readonly bool IsMyFriend;
	//10
	public readonly bool IsPurple;
	//11
	public readonly MagicClass MagicClass;
	//12

	public readonly bool IsClubMember;
	internal AddReceiveMessage(Message message) : base(message)
	{

		UserID = message.GetInteger(0);
		Username = message.GetString(1);
		Face = (Smiley)message.GetInteger(2);
		PlayerPosX = message.GetInteger(3);
		PlayerPosY = message.GetInteger(4);
		IsGod = message.GetBoolean(5);
		IsMod = message.GetBoolean(6);
		HasChat = message.GetBoolean(7);
		Coins = message.GetInteger(8);
		IsMyFriend = message.GetBoolean(9);
		IsPurple = message.GetBoolean(10);
		MagicClass = (MagicClass)message.GetInteger(11);
		IsClubMember = message.GetBoolean(12);
	}
}
