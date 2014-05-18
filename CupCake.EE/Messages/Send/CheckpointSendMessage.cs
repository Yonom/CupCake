using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public class CheckpointSendMessage : SendMessage
{

	public readonly int X;

	public readonly int Y;
	public CheckpointSendMessage(int x, int y)
	{
		this.X = x;
		this.Y = y;
	}

	internal override Message GetMessage()
	{
		return Message.Create("checkpoint", X, Y);
	}
}
