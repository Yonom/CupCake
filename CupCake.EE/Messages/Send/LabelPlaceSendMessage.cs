using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class LabelPlaceSendMessage : BlockPlaceSendMessage
{


	public readonly string Text;
	public LabelPlaceSendMessage(string encryption, Layer layer, int x, int y, LabelBlock block, string text) : base(encryption, layer, x, y, (Block)block)
	{

		this.Text = text;
	}

	internal override Message GetMessage()
	{
		if (IsLabel(Block)) {
			Message message = base.GetMessage();
			message.Add(Text);
			return message;
		} else {
			return base.GetMessage();
		}
	}
}
