using System;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class MoveReceiveEvent : ReceiveEvent, IUserPosReceiveEvent
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
#pragma warning disable 618
            this.IsDead = message.GetBoolean(11);
#pragma warning restore 618
        }

        public int Coins { get; set; }
        public double Horizontal { get; set; }
        public bool IsPurple { get; set; }
        public double ModifierX { get; set; }
        public double ModifierY { get; set; }
        public double SpeedX { get; set; }
        public double SpeedY { get; set; }
        public double Vertical { get; set; }

        [Obsolete("IsDead is no longer supported in the game code.")]
        public bool IsDead { get; set; }

        public int BlockX
        {
            get { return this.UserPosX + 8 >> 4; }
        }

        public int BlockY
        {
            get { return this.UserPosY + 8 >> 4; }
        }

        public int UserPosX { get; set; }
        public int UserPosY { get; set; }
        public int UserId { get; set; }
    }
}