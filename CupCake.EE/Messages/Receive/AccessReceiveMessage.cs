using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class AccessReceiveMessage : ReceiveMessage
{

	//No arguments

	internal AccessReceiveMessage(Message message) : base(message)
	{
	}
}
