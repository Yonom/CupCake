using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class SayReceiveMessage : ReceiveMessage
{

	//0
	public readonly int UserID;
	//1
	public readonly string Text;
	//2

	public readonly bool IsMyFriend;
	internal SayReceiveMessage(Message message) : base(message)
	{

		UserID = message.GetInteger(0);
		Text = message.GetString(1);
		IsMyFriend = message.GetBoolean(2);
	}
}
