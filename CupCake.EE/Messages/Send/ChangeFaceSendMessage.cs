using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class ChangeFaceSendMessage : SendMessage
{
    public string Encryption { get; set; }
    public readonly Smiley Face;
	public ChangeFaceSendMessage(string encryption, Smiley face)
	{
	    this.Encryption = encryption;
	    this.Face = face;
	}

    internal override Message GetMessage()
	{
        return Message.Create(Encryption + "f", Face);
	}
}
