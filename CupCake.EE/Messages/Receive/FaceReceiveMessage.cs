using CupCake.EE.Players;
using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class FaceReceiveMessage : ReceiveMessage
    {
        //0
        //1

        public readonly Smiley Face;
        public readonly int UserID;

        internal FaceReceiveMessage(Message message)
            : base(message)
        {
            this.UserID = message.GetInteger(0);
            this.Face = (Smiley)message.GetInteger(1);
        }
    }
}