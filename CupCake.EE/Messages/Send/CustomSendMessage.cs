using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

internal sealed class CustomSendMessage : SendMessage
{


	private readonly Message myMessage;
	public CustomSendMessage(string type, params string[] parameters)
	{
		myMessage = Message.Create(type, parameters);
	}

	internal override Message GetMessage()
	{
		return myMessage;
	}
}
