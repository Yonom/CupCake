using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class ResetReceiveMessage : ReceiveMessage
{

	//No arguments

	internal ResetReceiveMessage(Message message) : base(message)
	{
	}
}
