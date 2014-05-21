using CupCake.EE.Players;
using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public class FaceReceiveEvent : ReceiveEvent
    {
        public FaceReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.Face = (Smiley)message.GetInteger(1);
        }

        public Smiley Face { get; private set; }
        public int UserId { get; private set; }
    }
}