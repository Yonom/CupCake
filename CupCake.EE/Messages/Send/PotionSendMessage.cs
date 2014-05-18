using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class PotionSendMessage : SendMessage
{
    public string Encryption { get; set; }
    public readonly Potion Potion;
	public PotionSendMessage(string encryption, Potion potion)
	{
	    this.Encryption = encryption;
	    this.Potion = potion;
	}

    internal override Message GetMessage()
	{
		return Message.Create(Encryption + "p", Convert.ToInt32(Potion));
	}
}
