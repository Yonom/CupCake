using PlayerIOClient;

namespace CupCake.EE.Messages.Send
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

        public override Message GetMessage()
        {
            return Message.Create("diamondtouch", this.X, this.Y);
        }
    }
}