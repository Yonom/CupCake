using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class LostAccessReceiveMessage : ReceiveMessage
{

	//No arguments

	internal LostAccessReceiveMessage(Message message) : base(message)
	{
	}
}
