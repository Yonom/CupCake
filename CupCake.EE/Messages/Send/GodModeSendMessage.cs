using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class GodModeSendMessage : SendMessage
{


	public readonly bool GodModeEnabled;
	public GodModeSendMessage(bool godModeEnabled)
	{
		this.GodModeEnabled = godModeEnabled;
	}

	internal override Message GetMessage()
	{
		return Message.Create("god", GodModeEnabled);
	}
}
