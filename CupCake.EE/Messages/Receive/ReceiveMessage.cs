using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.ComponentModel;
using PlayerIOClient;

public abstract class ReceiveMessage : EventArgs
{

	[EditorBrowsable(EditorBrowsableState.Advanced)]

	public readonly Message PlayerIOMessage;
	internal ReceiveMessage(Message message)
	{
		PlayerIOMessage = message;
	}
}
