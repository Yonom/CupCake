using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class AutoSaySendMessage : SendMessage
{


	public readonly AutoText Text;
	public AutoSaySendMessage(AutoText text)
	{
		this.Text = text;
	}

	internal override Message GetMessage()
	{
		return Message.Create("autosay", Text);
	}
}
