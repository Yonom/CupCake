using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class WriteReceiveMessage : ReceiveMessage
{

	//0
	public readonly string Title;
	//1

	public readonly string Text;
	internal WriteReceiveMessage(Message message) : base(message)
	{

		Title = message.GetString(0);
		Text = message.GetString(1);
	}
}
