using PlayerIOClient;

namespace CupCake.EE.Messages.Send
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

        public override Message GetMessage()
        {
            return Message.Create("caketouch", this.X, this.Y);
        }
    }
}