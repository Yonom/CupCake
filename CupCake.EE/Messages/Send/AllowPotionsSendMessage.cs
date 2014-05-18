using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public class AllowPotionsSendMessage : SendMessage
{


	public readonly bool Allowed;
	public AllowPotionsSendMessage(bool allowed)
	{
		this.Allowed = allowed;
	}

	internal override Message GetMessage()
	{
		return Message.Create("allowpotions", Allowed);
	}
}
