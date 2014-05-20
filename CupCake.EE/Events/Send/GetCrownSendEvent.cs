using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public sealed class GetCrownSendEvent : SendEvent
    {
        public GetCrownSendEvent(string encryption)
        {
            this.Encryption = encryption;
        }

        public string Encryption { get; set; }

        public override Message GetMessage()
        {
            return Message.Create(this.Encryption + "k");
        }
    }
}