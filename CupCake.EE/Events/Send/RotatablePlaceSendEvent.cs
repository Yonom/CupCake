using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public class RotatablePlaceSendEvent : BlockPlaceSendEvent
    {
        public RotatablePlaceSendEvent(string encryption, Layer layer, int x, int y, RotatableBlock block,
            int rotation)
            : base(encryption, layer, x, y, (Block)block)
        {
            this.Rotation = rotation;
        }

        public int Rotation { get; set; }

        public override Message GetMessage()
        {
            if (IsRotatable(this.Block))
            {
                Message message = base.GetMessage();
                message.Add(this.Rotation);
                return message;
            }
            return base.GetMessage();
        }
    }
}