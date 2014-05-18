using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PlayerIOClient;

public sealed class TeleportPlayerReceiveMessage : ReceiveMessage
{

	//this.connection.addMessageHandler("teleport", function (param1:Message, param2:int, param3:Number, param4:Number) : void
	//        {
	//            var _loc_5:Player = null;
	//            if (param2 == myid)
	//            {
	//                player.setPosition(param3, param4);
	//            }
	//            else
	//            {
	//                _loc_5 = players[param2] as Player;
	//                if (_loc_5)
	//                {
	//                    _loc_5.setPosition(param3, param4);
	//                }
	//            }
	//            return;
	//        }

	//0
	public readonly int UserID;
	//1
	public readonly int PlayerPosX;
	//2

	public readonly int PlayerPosY;
	internal TeleportPlayerReceiveMessage(Message message) : base(message)
	{

		UserID = message.GetInteger(0);
		PlayerPosX = message.GetInteger(1);
		PlayerPosY = message.GetInteger(2);
	}

	public int BlockX {
		get { return PlayerPosX + 8 >> 4; }
	}

	public int BlockY {
		get { return PlayerPosY + 8 >> 4; }
	}
}
