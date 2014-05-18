using System;
using CupCake.Utils.Blocks;
using PlayerIOClient;

namespace CupCake.Utils.Messages.Send
{
    public sealed class PotionSendMessage : SendMessage
    {
        public readonly Potion Potion;

        public PotionSendMessage(string encryption, Potion potion)
        {
            this.Encryption = encryption;
            this.Potion = potion;
        }

        public string Encryption { get; set; }

        internal override Message GetMessage()
        {
            return Message.Create(this.Encryption + "p", Convert.ToInt32(this.Potion));
        }
    }
}