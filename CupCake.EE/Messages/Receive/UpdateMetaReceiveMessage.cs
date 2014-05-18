using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class UpdateMetaReceiveMessage : ReceiveMessage
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
	internal UpdateMetaReceiveMessage(Message message) : base(message)
	{

		OwnerUsername = message.GetString(0);
		WorldName = message.GetString(1);
		Plays = message.GetInteger(2);
		CurrentWoots = message.GetInteger(3);
		TotalWoots = message.GetInteger(4);
	}
}
