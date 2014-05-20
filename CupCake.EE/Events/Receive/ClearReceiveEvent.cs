using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public sealed class ClearReceiveEvent : ReceiveEvent
    {
        public ClearReceiveEvent(Message message)
            : base(message)
        {
            this.RoomWidth = message.GetInteger(0);
            this.RoomHeight = message.GetInteger(1);
        }

        public int RoomHeight { get; private set; }
        public int RoomWidth { get; private set; }
    }
}