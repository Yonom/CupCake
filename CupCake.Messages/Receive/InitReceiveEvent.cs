using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class InitReceiveEvent : ReceiveEvent, IUserPosReceiveEvent, IMetadataReceiveMessage
    {
        public InitReceiveEvent(Message message)
            : base(message)
        {
            OwnerUsername = message.GetString(0);
            WorldName = message.GetString(1);
            Plays = message.GetInteger(2);
            CurrentWoots = message.GetInteger(3);
            TotalWoots = message.GetInteger(4);
            Encryption = message.GetString(5);
            UserId = message.GetInteger(6);
            SpawnX = message.GetInteger(7);
            SpawnY = message.GetInteger(8);
            Username = message.GetString(9);
            CanEdit = message.GetBoolean(10);
            IsOwner = message.GetBoolean(11);
            RoomWidth = message.GetInteger(12);
            RoomHeight = message.GetInteger(13);
            IsTutorialRoom = message.GetBoolean(14);
            Gravity = message.GetDouble(15);
            AllowPotions = message.GetBoolean(16);
        }

        public bool AllowPotions { get; set; }
        public bool CanEdit { get; set; }
        public string Encryption { get; set; }
        public double Gravity { get; set; }
        public bool IsOwner { get; set; }
        public bool IsTutorialRoom { get; set; }
        public int RoomWidth { get; set; }
        public int RoomHeight { get; set; }
        public int SpawnX { get; set; }
        public int SpawnY { get; set; }
        public string Username { get; set; }
        public int CurrentWoots { get; set; }
        public string OwnerUsername { get; set; }
        public int Plays { get; set; }
        public int TotalWoots { get; set; }
        public string WorldName { get; set; }
        public int UserId { get; set; }

        int IUserPosReceiveEvent.UserPosX
        {
            get { return SpawnX; }
            set { SpawnX = value; }
        }

        int IUserPosReceiveEvent.UserPosY
        {
            get { return SpawnY; }
            set { SpawnY = value; }
        }
    }
}