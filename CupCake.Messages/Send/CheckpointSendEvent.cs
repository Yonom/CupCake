using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class CheckpointSendEvent : SendEvent
    {
        public CheckpointSendEvent(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override Message GetMessage()
        {
            return Message.Create("checkpoint", X, Y);
        }
    }
}