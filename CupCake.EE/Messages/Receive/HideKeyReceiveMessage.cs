using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class HideKeyReceiveMessage : ReceiveMessage
{

	//0

	public readonly Key[] Keys;
	internal HideKeyReceiveMessage(Message message) : base(message)
	{

		Keys = new Key[Convert.ToInt32(message.Count - 1u) + 1];
		for (uint i = 0; i <= message.Count - 1u; i++) {
			Keys[Convert.ToInt32(i)] = (Key)Enum.Parse(typeof(Key), message.GetString(i), true);
		}
	}
}
