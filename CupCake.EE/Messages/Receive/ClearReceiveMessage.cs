using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class ClearReceiveMessage : ReceiveMessage
    {
        public int RoomHeight { get; private set; }
        public int RoomWidth { get; private set; }

        public ClearReceiveMessage(Message message)
            : base(message)
        {
            this.RoomWidth = message.GetInteger(0);
            this.RoomHeight = message.GetInteger(1);
        }
    }
}