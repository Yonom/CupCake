using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class InitReceiveMessage : ReceiveMessage
    {
        //0
        //3
        public readonly bool AllowPotions;
        public readonly bool CanEdit;
        public readonly int CurrentWoots;
        //4
        //5
        public readonly string Encryption;
        public readonly double Gravity;
        //6
        //11
        public readonly bool IsOwner;
        public readonly bool IsTutorialRoom;
        public readonly string OwnerUsername;
        public readonly int Plays;
        //12
        public readonly int SizeX;
        //13
        public readonly int SizeY;
        public readonly int SpawnX;
        //8
        public readonly int SpawnY;
        public readonly int TotalWoots;
        public readonly int UserID;
        public readonly string Username;
        public readonly string WorldName;
        //14

        internal InitReceiveMessage(Message message)
            : base(message)
        {
            this.OwnerUsername = message.GetString(0);
            this.WorldName = message.GetString(1);
            this.Plays = message.GetInteger(2);
            this.CurrentWoots = message.GetInteger(3);
            this.TotalWoots = message.GetInteger(4);
            this.Encryption = message.GetString(5);
            this.UserID = message.GetInteger(6);
            this.SpawnX = message.GetInteger(7);
            this.SpawnY = message.GetInteger(8);
            this.Username = message.GetString(9);
            this.CanEdit = message.GetBoolean(10);
            this.IsOwner = message.GetBoolean(11);
            this.SizeX = message.GetInteger(12);
            this.SizeY = message.GetInteger(13);
            this.IsTutorialRoom = message.GetBoolean(14);
            this.Gravity = message.GetDouble(15);
            this.AllowPotions = message.GetBoolean(16);
        }
    }
}