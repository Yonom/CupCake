using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class SoundPlaceSendMessage : BlockPlaceSendMessage
{


	public readonly int SoundID;
    public SoundPlaceSendMessage(string encryption, Layer layer, int x, int y, SoundBlock block, int soundID)
        : base(encryption, layer, x, y, (Block)block)
	{

		this.SoundID = soundID;
	}

	internal override Message GetMessage()
	{
		if (IsSound(Block)) {
			Message message = base.GetMessage();
			message.Add(SoundID);
			return message;
		} else {
			return base.GetMessage();
		}
	}
}
