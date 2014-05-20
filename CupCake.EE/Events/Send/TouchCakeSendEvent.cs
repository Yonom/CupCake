using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public sealed class TouchCakeSendEvent : SendEvent
    {
        public TouchCakeSendEvent(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override Message GetMessage()
        {
            return Message.Create("caketouch", this.X, this.Y);
        }
    }
}