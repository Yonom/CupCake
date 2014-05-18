using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class PressRedKeySendMessage : SendMessage
{
    public string Encryption { get; set; }

    public PressRedKeySendMessage(string encryption)
    {
        this.Encryption = encryption;
    }

    internal override Message GetMessage()
	{
		return Message.Create(Encryption + "r");
	}
}
