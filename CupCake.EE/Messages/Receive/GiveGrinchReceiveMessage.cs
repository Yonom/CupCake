using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class GiveGrinchReceiveMessage : ReceiveMessage
{

	//No arguments

	internal GiveGrinchReceiveMessage(Message message) : base(message)
	{
	}
}
