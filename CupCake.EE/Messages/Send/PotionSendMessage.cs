using System;
using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class PotionSendMessage : SendMessage
    {
        public PotionSendMessage(string encryption, Potion potion)
        {
            this.Encryption = encryption;
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