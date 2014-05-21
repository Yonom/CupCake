using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public class PressRedKeySendEvent : SendEvent
    {
        public PressRedKeySendEvent(string encryption)
        {
            this.Encryption = encryption;
        }

        public string Encryption { get; set; }

        public override Message GetMessage()
        {
            return Message.Create(this.Encryption + "r");
        }
    }
}