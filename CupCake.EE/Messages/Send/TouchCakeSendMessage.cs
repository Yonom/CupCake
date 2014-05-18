using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class TouchCakeSendMessage : SendMessage
{

	public readonly int X;

	public readonly int Y;
	public TouchCakeSendMessage(int x, int y)
	{
		this.X = x;
		this.Y = y;
	}

	internal override Message GetMessage()
	{
		return Message.Create("caketouch", X, Y);
	}
}
