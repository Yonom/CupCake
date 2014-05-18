using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class GiveDarkWizardReceiveMessage : ReceiveMessage
{

	//No arguments

	internal GiveDarkWizardReceiveMessage(Message message) : base(message)
	{
	}
}
