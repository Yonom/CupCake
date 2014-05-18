using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public class RotatablePlaceSendMessage : BlockPlaceSendMessage
{


	public readonly int Rotation;
    public RotatablePlaceSendMessage(string encryption, Layer layer, int x, int y, RotatableBlock block, int rotation)
        : base(encryption, layer, x, y, (Block)block)
	{

		this.Rotation = rotation;
	}

	internal override Message GetMessage()
	{
		if (IsRotatable(Block)) {
			Message message = base.GetMessage();
			message.Add(Rotation);
			return message;
		} else {
			return base.GetMessage();
		}
	}
}
