using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public sealed class TouchDiamondSendEvent : SendEvent
    {
        public TouchDiamondSendEvent(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override Message GetMessage()
        {
            return Message.Create("diamondtouch", this.X, this.Y);
        }
    }
}