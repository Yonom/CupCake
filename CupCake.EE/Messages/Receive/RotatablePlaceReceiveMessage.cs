using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public class RotatablePlaceReceiveMessage : BlockPlaceReceiveMessage
    {
        public RotatableBlock RotatableBlock { get; private set; }
        public int Rotation { get; private set; }

        public RotatablePlaceReceiveMessage(Message message)
            : base(message, Layer.Foreground, message.GetInteger(0), message.GetInteger(1), (Block)message.GetInteger(2)
                )
        {
            this.RotatableBlock = (RotatableBlock)message.GetInteger(2);
            this.Rotation = message.GetInteger(3);
        }
    }
}