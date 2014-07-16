using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class UpdateMetaReceiveEvent : ReceiveEvent, IMetadataReceiveMessage
    {
        public UpdateMetaReceiveEvent(Message message)
            : base(message)
        {
            OwnerUsername = message.GetString(0);
            WorldName = message.GetString(1);
            Plays = message.GetInteger(2);
            CurrentWoots = message.GetInteger(3);
            TotalWoots = message.GetInteger(4);
        }

        public int CurrentWoots { get; set; }
        public string OwnerUsername { get; set; }
        public int Plays { get; set; }
        public int TotalWoots { get; set; }
        public string WorldName { get; set; }
    }
}