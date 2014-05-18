using PlayerIOClient;

namespace CupCake.Utils.Messages.Send
{
    public sealed class TouchCakeSendMessage : SendMessage
    {
        public readonly int X;

        public readonly int Y;

        public TouchCakeSendMessage(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        internal override Message GetMessage()
        {
            return Message.Create("caketouch", this.X, this.Y);
        }
    }
}