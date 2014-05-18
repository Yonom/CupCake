using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class ChangeWorldEditKeySendMessage : SendMessage
{


	public readonly string EditKey;
	public ChangeWorldEditKeySendMessage(string editKey)
	{
		this.EditKey = editKey;
	}

	internal override Message GetMessage()
	{
		return Message.Create("key", EditKey);
	}
}
