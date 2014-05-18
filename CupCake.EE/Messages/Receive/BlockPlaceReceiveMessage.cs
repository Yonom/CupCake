using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public class BlockPlaceReceiveMessage : ReceiveMessage
{

	//0
	public readonly Layer Layer;
	//1
	public readonly int PosX;
	//2
	public readonly int PosY;
	//3

	public readonly Block Block;
	internal BlockPlaceReceiveMessage(Message message) : base(message)
	{

		Layer = (Layer)message.GetInteger(0);
		PosX = message.GetInteger(1);
		PosY = message.GetInteger(2);
		Block = (Block)message.GetInteger(3);
	}

	protected BlockPlaceReceiveMessage(Message message, Layer layer, int posX, int posY, Block block) : base(message)
	{

		this.Layer = layer;
		this.PosX = posX;
		this.PosY = posY;
		this.Block = block;
	}
}
