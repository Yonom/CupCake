using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class MoveReceiveEvent : ReceiveEvent, IUserPosReceiveEvent
    {
        public MoveReceiveEvent(Message message)
            : base(message)
        {
            UserId = message.GetInteger(0);
            UserPosX = message.GetInteger(1);
            UserPosY = message.GetInteger(2);
            SpeedX = message.GetDouble(3);
            SpeedY = message.GetDouble(4);
            ModifierX = message.GetDouble(5);
            ModifierY = message.GetDouble(6);
            Horizontal = message.GetDouble(7);
            Vertical = message.GetDouble(8);
            Coins = message.GetInteger(9);
            IsPurple = message.GetBoolean(10);
            IsDead = message.GetBoolean(11);
        }

        public int Coins { get; set; }
        public double Horizontal { get; set; }
        public bool IsPurple { get; set; }
        public double ModifierX { get; set; }
        public double ModifierY { get; set; }
        public double SpeedX { get; set; }
        public double SpeedY { get; set; }
        public double Vertical { get; set; }
        public bool IsDead { get; set; }

        public int BlockX
        {
            get { return BlockUtils.PosToBlock(UserPosX); }
        }

        public int BlockY
        {
            get { return BlockUtils.PosToBlock(UserPosY); }
        }

        public int UserPosX { get; set; }
        public int UserPosY { get; set; }
        public int UserId { get; set; }
    }
}