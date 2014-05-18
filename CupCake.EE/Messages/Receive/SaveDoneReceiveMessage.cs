using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class SaveDoneReceiveMessage : ReceiveMessage
{

	//No arguments

	internal SaveDoneReceiveMessage(Message message) : base(message)
	{
	}
}
