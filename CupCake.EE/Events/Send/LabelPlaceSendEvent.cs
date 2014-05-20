using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public sealed class LabelPlaceSendEvent : BlockPlaceSendEvent
    {
        public LabelPlaceSendEvent(string encryption, Layer layer, int x, int y, LabelBlock block, string text)
            : base(encryption, layer, x, y, (Block)block)
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