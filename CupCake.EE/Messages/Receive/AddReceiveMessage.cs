using CupCake.EE.Players;
using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class AddReceiveMessage : ReceiveMessage
    {
        //0
        //2
        public readonly int Coins;
        public readonly Smiley Face;
        public readonly bool HasChat;
        public readonly bool IsClubMember;
        //3
        //5
        public readonly bool IsGod;
        //6
        public readonly bool IsMod;
        //7
        //9
        public readonly bool IsMyFriend;
        //10
        public readonly bool IsPurple;
        //11
        public readonly MagicClass MagicClass;
        public readonly int PlayerPosX;
        //4
        public readonly int PlayerPosY;
        public readonly int UserID;
        //1
        public readonly string Username;
        //12

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