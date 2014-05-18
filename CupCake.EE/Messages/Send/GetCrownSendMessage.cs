using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class GetCrownSendMessage : SendMessage
{
    public string Encryption { get; set; }

    public GetCrownSendMessage(string encryption)
    {
        this.Encryption = encryption;
    }

    internal override Message GetMessage()
	{
		return Message.Create(Encryption + "k");
	}
}
