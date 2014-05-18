using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class AccessSendMessage : SendMessage
{


	public readonly string EditKey;
	public AccessSendMessage(string editKey)
	{
		this.EditKey = editKey;
	}

	internal override Message GetMessage()
	{
		return Message.Create("access", EditKey);
	}
}
