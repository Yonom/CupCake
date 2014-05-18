using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class GiveFireWizardReceiveMessage : ReceiveMessage
{

	//No arguments

	internal GiveFireWizardReceiveMessage(Message message) : base(message)
	{
	}
}
