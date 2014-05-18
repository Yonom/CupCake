using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class PressGreenKeySendMessage : SendMessage
    {
        public PressGreenKeySendMessage(string encryption)
        {
            this.Encryption = encryption;
        }

        public string Encryption { get; set; }

        public override Message GetMessage()
        {
            return Message.Create(this.Encryption + "g");
        }
    }
}