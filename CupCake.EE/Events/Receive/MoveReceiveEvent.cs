using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public class MoveReceiveEvent : ReceiveEvent, IUserPosEvent
    {
        public MoveReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.UserPosX = message.GetInteger(1);
            this.UserPosY = message.GetInteger(2);
            this.SpeedX = message.GetDouble(3);
            this.SpeedY = message.GetDouble(4);
            this.ModifierX = message.GetDouble(5);
            this.ModifierY = message.GetDouble(6);
            this.Horizontal = message.GetDouble(7);
            this.Vertical = message.GetDouble(8);
            this.Coins = message.GetInteger(9);
            this.IsPurple = message.GetBoolean(10);
        }

        public int Coins { get; private set; }
        public double Horizontal { get; private set; }
        public bool IsPurple { get; private set; }
        public double ModifierX { get; private set; }
        public double ModifierY { get; private set; }
        public int UserPosX { get; private set; }
        public int UserPosY { get; private set; }
        public double SpeedX { get; private set; }
        public double SpeedY { get; private set; }
        public int UserId { get; private set; }
        public double Vertical { get; private set; }

        public int BlockX
        {
            get { return this.UserPosX + 8 >> 4; }
        }

        public int BlockY
        {
            get { return this.UserPosY + 8 >> 4; }
        }
    }
}