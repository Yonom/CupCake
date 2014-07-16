using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class TouchCakeSendEvent : SendEvent
    {
        public TouchCakeSendEvent(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override Message GetMessage()
        {
            return Message.Create("caketouch", X, Y);
        }
    }
}