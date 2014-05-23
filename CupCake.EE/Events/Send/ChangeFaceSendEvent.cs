using CupCake.EE.User;
using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public class ChangeFaceSendEvent : SendEvent, IEncryptedSendEvent
    {
        public ChangeFaceSendEvent(Smiley face)
        {
            this.Face = face;
        }

        public Smiley Face { get; set; }

        public string Encryption { get; set; }

        public override Message GetMessage()
        {
            return Message.Create(this.Encryption + "f", this.Face);
        }
    }
}