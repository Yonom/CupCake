using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class MoveReceiveMessage : ReceiveMessage
    {
        public int Coins { get; private set; }
        public double Horizontal { get; private set; }
        public bool IsPurple { get; private set; }
        public double ModifierX { get; private set; }
        public double ModifierY { get; private set; }
        public int PlayerPosX { get; private set; }
        public int PlayerPosY { get; private set; }
        public double SpeedX { get; private set; }
        public double SpeedY { get; private set; }
        public int UserId { get; private set; }
        public double Vertical { get; private set; }

        public MoveReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
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