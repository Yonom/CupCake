using CupCake.Utils.Players;
using PlayerIOClient;

namespace CupCake.Utils.Messages.Receive
{
    public sealed class FaceReceiveMessage : ReceiveMessage
    {
        public readonly Smiley Face;
        public readonly int UserId;

        internal FaceReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.Face = (Smiley)message.GetInteger(1);
        }
    }
}