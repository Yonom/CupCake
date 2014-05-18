using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class RefreshShopReceiveMessage : ReceiveMessage
{

	//No arguments; this is just a request to refresh the shop on the client-side.

	internal RefreshShopReceiveMessage(Message message) : base(message)
	{
	}
}
