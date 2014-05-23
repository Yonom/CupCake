using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public class GetCrownSendEvent : SendEvent, IEncryptedSendEvent
    {
        public string Encryption { get; set; }

        public override Message GetMessage()
        {
            return Message.Create(this.Encryption + "k");
        }
    }
}