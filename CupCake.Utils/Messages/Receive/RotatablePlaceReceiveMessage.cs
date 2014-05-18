using CupCake.Utils.Blocks;
using PlayerIOClient;

namespace CupCake.Utils.Messages.Receive
{
    public class RotatablePlaceReceiveMessage : BlockPlaceReceiveMessage
    {
        //2
        public readonly RotatableBlock RotatableBlock;
        //3

        public readonly int Rotation;

        internal RotatablePlaceReceiveMessage(Message message)
            : base(message, Layer.Foreground, message.GetInteger(0), message.GetInteger(1), (Block)message.GetInteger(2)
                )
        {
            this.RotatableBlock = (RotatableBlock)message.GetInteger(2);
            this.Rotation = message.GetInteger(3);
        }
    }
}