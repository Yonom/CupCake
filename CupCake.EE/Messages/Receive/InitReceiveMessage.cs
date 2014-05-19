using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class InitReceiveMessage : ReceiveMessage
    {
        public readonly bool AllowPotions;
        public readonly bool CanEdit;
        public readonly int CurrentWoots;
        public readonly string Encryption;
        public readonly double Gravity;
        public readonly bool IsOwner;
        public readonly bool IsTutorialRoom;
        public readonly string OwnerUsername;
        public readonly int Plays;
        public readonly int SizeX;
        public readonly int SizeY;
        public readonly int SpawnX;
        public readonly int SpawnY;
        public readonly int TotalWoots;
        public readonly int UserId;
        public readonly string Username;
        public readonly string WorldName;

        public InitReceiveMessage(Message message)
            : base(message)
        {
            this.OwnerUsername = message.GetString(0);
            this.WorldName = message.GetString(1);
            this.Plays = message.GetInteger(2);
            this.CurrentWoots = message.GetInteger(3);
            this.TotalWoots = message.GetInteger(4);
            this.Encryption = message.GetString(5);
            this.UserId = message.GetInteger(6);
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