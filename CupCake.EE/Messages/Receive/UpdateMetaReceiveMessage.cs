using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class UpdateMetaReceiveMessage : ReceiveMessage
    {
        //0
        public readonly int CurrentWoots;
        public readonly string OwnerUsername;
        //1
        //2
        public readonly int Plays;
        //3
        //4

        public readonly int TotalWoots;
        public readonly string WorldName;

        internal UpdateMetaReceiveMessage(Message message)
            : base(message)
        {
            this.OwnerUsername = message.GetString(0);
            this.WorldName = message.GetString(1);
            this.Plays = message.GetInteger(2);
            this.CurrentWoots = message.GetInteger(3);
            this.TotalWoots = message.GetInteger(4);
        }
    }
}