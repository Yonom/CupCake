using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public class CheckpointSendMessage : SendMessage
    {
        public CheckpointSendMessage(int x, int y)
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