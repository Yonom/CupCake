using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public class PressBlueKeySendEvent : SendEvent
    {
        public PressBlueKeySendEvent(string encryption)
        {
            this.Encryption = encryption;
        }

        public string Encryption { get; set; }

        public override Message GetMessage()
        {
            return Message.Create(this.Encryption + "b");
        }
    }
}