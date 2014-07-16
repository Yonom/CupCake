using CupCake.Messages.User;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class AddReceiveEvent : ReceiveEvent, IUserPosReceiveEvent
    {
        public AddReceiveEvent(Message message)
            : base(message)
        {
            UserId = message.GetInteger(0);
            Username = message.GetString(1);
            Face = (Smiley) message.GetInteger(2);
            UserPosX = message.GetInteger(3);
            UserPosY = message.GetInteger(4);
            IsGod = message.GetBoolean(5);
            IsMod = message.GetBoolean(6);
            HasChat = message.GetBoolean(7);
            Coins = message.GetInteger(8);
            IsMyFriend = message.GetBoolean(9);
            IsPurple = message.GetBoolean(10);
            MagicClass = (MagicClass) message.GetInteger(11);
            IsClubMember = message.GetBoolean(12);
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