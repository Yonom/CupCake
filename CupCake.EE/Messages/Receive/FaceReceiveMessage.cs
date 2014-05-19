using CupCake.EE.Players;
using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class FaceReceiveMessage : ReceiveMessage
    {
        public Smiley Face { get; private set; }
        public int UserId { get; private set; }

        public FaceReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.Face = (Smiley)message.GetInteger(1);
        }
    }
}