using System;
using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public class PotionSendEvent : SendEvent, IEncryptedSendEvent
    {
        public PotionSendEvent(Potion potion)
        {
            this.Potion = potion;
        }

        public Potion Potion { get; set; }

        public string Encryption { get; set; }

        public override Message GetMessage()
        {
            return Message.Create(this.Encryption + "p", Convert.ToInt32(this.Potion));
        }
    }
}