using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class MoveReceiveMessage : ReceiveMessage
    {
        public readonly int Coins;
        public readonly double Horizontal;
        public readonly bool IsPurple;
        public readonly double ModifierX;
        public readonly double ModifierY;
        public readonly int PlayerPosX;
        public readonly int PlayerPosY;
        public readonly double SpeedX;
        public readonly double SpeedY;
        public readonly int UserID;
        public readonly double Vertical;

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
}