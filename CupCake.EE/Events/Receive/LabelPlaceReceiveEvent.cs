using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public class LabelPlaceReceiveEvent : BlockPlaceReceiveEvent
    {
        public LabelPlaceReceiveEvent(Message message)
            : base(message, Layer.Foreground, message.GetInteger(0), message.GetInteger(1), (Block)message.GetInteger(2)
                )
        {
            this.LabelBlock = (LabelBlock)message.GetInteger(2);
            this.Text = message.GetString(3);
        }

        public LabelBlock LabelBlock { get; private set; }
        public string Text { get; private set; }
    }
}