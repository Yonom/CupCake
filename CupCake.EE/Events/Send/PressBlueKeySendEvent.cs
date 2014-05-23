using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public class PressBlueKeySendEvent : SendEvent, IEncryptedSendEvent
    {
        public string Encryption { get; set; }

        public override Message GetMessage()
        {
            return Message.Create(this.Encryption + "b");
        }
    }
}