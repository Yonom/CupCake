using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public class CheckpointSendEvent : SendEvent
    {
        public CheckpointSendEvent(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override Message GetMessage()
        {
            return Message.Create("checkpoint", this.X, this.Y);
        }
    }
}