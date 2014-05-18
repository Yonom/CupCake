using PlayerIOClient;

public sealed class MoveReceiveMessage : ReceiveMessage
{
    //0
    //1
    public readonly int Coins;
    public readonly double Horizontal;
    public readonly bool IsPurple;
    public readonly double ModifierX;
    //6
    public readonly double ModifierY;
    public readonly int PlayerPosX;
    //2
    public readonly int PlayerPosY;
    //3
    public readonly double SpeedX;
    //4
    public readonly double SpeedY;
    public readonly int UserID;
    //5
    //8
    public readonly double Vertical;
    //9

    internal MoveReceiveMessage(Message message)
        : base(message)
    {
        this.UserID = message.GetInteger(0);
        this.PlayerPosX = message.GetInteger(1);
        this.PlayerPosY = message.GetInteger(2);
        this.SpeedX = message.GetDouble(3);
        this.SpeedY = message.GetDouble(4);
        this.ModifierX = message.GetDouble(5);
        this.ModifierY = message.GetDouble(6);
        this.Horizontal = message.GetDouble(7);
        this.Vertical = message.GetDouble(8);
        this.Coins = message.GetInteger(9);
        this.IsPurple = message.GetBoolean(10);
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