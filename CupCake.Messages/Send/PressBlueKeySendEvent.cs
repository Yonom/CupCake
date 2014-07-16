using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class PressBlueKeySendEvent : SendEvent, IEncryptedSendEvent
    {
        public string Encryption { get; set; }

        public override Message GetMessage()
        {
            return Message.Create(Encryption + "b");
        }
    }
}