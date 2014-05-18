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
    //1
    public readonly int PlayerPosX;
    //2

    public readonly int PlayerPosY;
    public readonly int UserID;

    internal TeleportPlayerReceiveMessage(Message message)
        : base(message)
    {
        this.UserID = message.GetInteger(0);
        this.PlayerPosX = message.GetInteger(1);
        this.PlayerPosY = message.GetInteger(2);
    }

    public int BlockX
    {
        get { return this.PlayerPosX + 8 >> 4; }
    }

    public int BlockY
    {
        get { return this.PlayerPosY + 8 >> 4; }
    }
}