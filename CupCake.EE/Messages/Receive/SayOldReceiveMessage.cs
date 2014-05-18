using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class SayOldReceiveMessage : ReceiveMessage
{

	//0
	public readonly string Username;
	//1
	public readonly string Text;
	//2

	public readonly bool IsMyFriend;
	internal SayOldReceiveMessage(Message message) : base(message)
	{

		Username = message.GetString(0);
		Text = message.GetString(1);
		IsMyFriend = message.GetBoolean(2);
	}
}
