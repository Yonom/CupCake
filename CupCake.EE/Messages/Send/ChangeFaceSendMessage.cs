using CupCake.EE.Players;
using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class ChangeFaceSendMessage : SendMessage
    {
        public Smiley Face { get; set; }

        public ChangeFaceSendMessage(string encryption, Smiley face)
        {
            this.Encryption = encryption;
            this.Face = face;
        }

        public string Encryption { get; set; }

        public override Message GetMessage()
        {
            return Message.Create(this.Encryption + "f", this.Face);
        }
    }
}