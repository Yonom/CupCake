using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public class TouchPlayerSendMessage : SendMessage
{

	public readonly int UserID;

	public readonly Potion Reason;
	public TouchPlayerSendMessage(int userID, Potion reason)
	{
		this.UserID = userID;
		this.Reason = reason;
	}

	internal override Message GetMessage()
	{
		return Message.Create("touch", UserID, Reason);
	}
}
