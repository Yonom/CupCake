using CupCake.Messages.User;
using PlayerIOClient;

namespace CupCake.Messages.Events.Receive
{
    public class AddReceiveEvent : ReceiveEvent, IUserPosEvent
    {
        public AddReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.Username = message.GetString(1);
            this.Face = (Smiley)message.GetInteger(2);
            this.UserPosX = message.GetInteger(3);
            this.UserPosY = message.GetInteger(4);
            this.IsGod = message.GetBoolean(5);
            this.IsMod = message.GetBoolean(6);
            this.HasChat = message.GetBoolean(7);
            this.Coins = message.GetInteger(8);
            this.IsMyFriend = message.GetBoolean(9);
            this.IsPurple = message.GetBoolean(10);
            this.MagicClass = (MagicClass)message.GetInteger(11);
            this.IsClubMember = message.GetBoolean(12);
        }

        public int Coins { get; set; }
        public Smiley Face { get; set; }
        public bool HasChat { get; set; }
        public bool IsClubMember { get; set; }
        public bool IsGod { get; set; }
        public bool IsMod { get; set; }
        public bool IsMyFriend { get; set; }
        public bool IsPurple { get; set; }
        public MagicClass MagicClass { get; set; }
        public string Username { get; set; }
        public int UserPosX { get; set; }
        public int UserPosY { get; set; }
        public int UserId { get; set; }
    }
}