using CupCake.Utils.Blocks;
using PlayerIOClient;

namespace CupCake.Utils.Messages.Receive
{
    public sealed class LabelPlaceReceiveMessage : BlockPlaceReceiveMessage
    {
        //2
        public readonly LabelBlock LabelBlock;
        //3

        public readonly string Text;

        internal LabelPlaceReceiveMessage(Message message)
            : base(message, Layer.Foreground, message.GetInteger(0), message.GetInteger(1), (Block)message.GetInteger(2)
                )
        {
            this.LabelBlock = (LabelBlock)message.GetInteger(2);
            this.Text = message.GetString(3);
        }
    }
}