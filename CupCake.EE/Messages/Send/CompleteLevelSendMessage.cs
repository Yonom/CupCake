using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class CompleteLevelSendMessage : SendMessage
{

	//No arguments

	internal override Message GetMessage()
	{
		return Message.Create("levelcomplete");
	}
}
