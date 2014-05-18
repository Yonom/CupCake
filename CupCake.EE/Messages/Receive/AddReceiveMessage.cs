using CupCake.EE.Players;
using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class AddReceiveMessage : ReceiveMessage
    {
        public readonly int Coins;
        public readonly Smiley Face;
        public readonly bool HasChat;
        public readonly bool IsClubMember;
        public readonly bool IsGod;
        public readonly bool IsMod;
        public readonly bool IsMyFriend;
        public readonly bool IsPurple;
        public readonly MagicClass MagicClass;
        public readonly int PlayerPosX;
        public readonly int PlayerPosY;
        public readonly int UserID;
        public readonly string Username;

        internal AddReceiveMessage(Message message)
            : base(message)
        {
            this.UserID = message.GetInteger(0);
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
    }
}