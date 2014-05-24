using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Events.Receive
{
    public class RotatablePlaceReceiveEvent : BlockPlaceReceiveEvent
    {
        public RotatablePlaceReceiveEvent(Message message)
            : base(message, Layer.Foreground, message.GetInteger(0), message.GetInteger(1), (Block)message.GetInteger(2)
                )
        {
            this.RotatableBlock = (RotatableBlock)message.GetInteger(2);
            this.Rotation = message.GetInteger(3);
        }

        public RotatableBlock RotatableBlock { get; private set; }
        public int Rotation { get; private set; }
    }
}