using PlayerIOClient;

namespace CupCake.Utils.Messages.Send
{
    public sealed class PressBlueKeySendMessage : SendMessage
    {
        public PressBlueKeySendMessage(string encryption)
        {
            this.Encryption = encryption;
        }

        public string Encryption { get; set; }

        internal override Message GetMessage()
        {
            return Message.Create(this.Encryption + "b");
        }
    }
}