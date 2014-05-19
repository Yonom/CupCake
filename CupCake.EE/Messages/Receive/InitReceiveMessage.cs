using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class InitReceiveMessage : ReceiveMessage
    {
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

        public bool AllowPotions { get; private set; }
        public bool CanEdit { get; private set; }
        public int CurrentWoots { get; private set; }
        public string Encryption { get; private set; }
        public double Gravity { get; private set; }
        public bool IsOwner { get; private set; }
        public bool IsTutorialRoom { get; private set; }
        public string OwnerUsername { get; private set; }
        public int Plays { get; private set; }
        public int SizeX { get; private set; }
        public int SizeY { get; private set; }
        public int SpawnX { get; private set; }
        public int SpawnY { get; private set; }
        public int TotalWoots { get; private set; }
        public int UserId { get; private set; }
        public string Username { get; private set; }
        public string WorldName { get; private set; }
    }
}