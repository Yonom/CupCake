using CupCake.Messages.User;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class PotionSendEvent : SendEvent, IEncryptedSendEvent
    {
        public PotionSendEvent(Potion potion)
        {
            Potion = potion;
        }

        public Potion Potion { get; set; }

        public string Encryption { get; set; }

        public override Message GetMessage()
        {
            return Message.Create(Encryption + "p", (int) Potion);
        }
    }
}