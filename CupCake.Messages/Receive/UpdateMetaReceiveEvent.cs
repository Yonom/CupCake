using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class UpdateMetaReceiveEvent : ReceiveEvent
    {
        public UpdateMetaReceiveEvent(Message message)
            : base(message)
        {
            this.OwnerUsername = message.GetString(0);
            this.WorldName = message.GetString(1);
            this.Plays = message.GetInteger(2);
            this.CurrentWoots = message.GetInteger(3);
            this.TotalWoots = message.GetInteger(4);
        }

        public int CurrentWoots { get; set; }
        public string OwnerUsername { get; set; }
        public int Plays { get; set; }
        public int TotalWoots { get; set; }
        public string WorldName { get; set; }
    }
}