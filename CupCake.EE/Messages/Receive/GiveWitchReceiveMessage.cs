using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class GiveWitchReceiveMessage : ReceiveMessage
{

	//No arguments

	internal GiveWitchReceiveMessage(Message message) : base(message)
	{
	}
}
