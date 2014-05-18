using PlayerIOClient;

namespace CupCake.Utils.Messages.Send
{
    public sealed class TouchDiamondSendMessage : SendMessage
    {
        public readonly int X;

        public readonly int Y;

        public TouchDiamondSendMessage(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        internal override Message GetMessage()
        {
            return Message.Create("diamondtouch", this.X, this.Y);
        }
    }
}