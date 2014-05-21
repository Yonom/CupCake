using CupCake.EE.Players;
using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public class AddReceiveEvent : ReceiveEvent
    {
        public AddReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.Username = message.GetString(1);
            this.Face = (Smiley)message.GetInteger(2);
            this.PlayerPosX = message.GetInteger(3);
            this.PlayerPosY = message.GetInteger(4);
            this.IsGod = message.GetBoolean(5);
            this.IsMod = message.GetBoolean(6);
            this.HasChat = message.GetBoolean(7);
            this.Coins = message.GetInteger(8);
            this.IsMyFriend = message.GetBoolean(9);
            this.IsPurple = message.GetBoolean(10);
            this.MagicClass = (MagicClass)message.GetInteger(11);
            this.IsClubMember = message.GetBoolean(12);
        }

        public int Coins { get; private set; }
        public Smiley Face { get; private set; }
        public bool HasChat { get; private set; }
        public bool IsClubMember { get; private set; }
        public bool IsGod { get; private set; }
        public bool IsMod { get; private set; }
        public bool IsMyFriend { get; private set; }
        public bool IsPurple { get; private set; }
        public MagicClass MagicClass { get; private set; }
        public int PlayerPosX { get; private set; }
        public int PlayerPosY { get; private set; }
        public int UserId { get; private set; }
        public string Username { get; private set; }
    }
}