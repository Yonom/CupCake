using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class ClearReceiveEvent : ReceiveEvent
    {
        public ClearReceiveEvent(Message message)
            : base(message)
        {
            RoomWidth = message.GetInteger(0);
            RoomHeight = message.GetInteger(1);
            BorderBlock = (Block) message.GetInteger(2);
            FillBlock = (Block) message.GetInteger(3);
        }

        public Block FillBlock { get; set; }
        public Block BorderBlock { get; set; }

        public int RoomHeight { get; set; }
        public int RoomWidth { get; set; }
    }
}