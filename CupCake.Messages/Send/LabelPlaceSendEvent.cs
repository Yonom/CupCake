using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class LabelPlaceSendEvent : BlockPlaceSendEvent
    {
        public LabelPlaceSendEvent(Layer layer, int x, int y, LabelBlock block, string text)
            : base(layer, x, y, (Block)block)
        {
            this.Text = text;
        }

        public string Text { get; set; }

        public override Message GetMessage()
        {
            if (IsLabel(this.Block))
            {
                Message message = base.GetMessage();
                message.Add(this.Text);
                return message;
            }
            return base.GetMessage();
        }
    }
}