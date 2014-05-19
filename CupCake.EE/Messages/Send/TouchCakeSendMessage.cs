using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class TouchCakeSendMessage : SendMessage
    {
        public int X { get; set; }
        public int Y { get; set; }

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