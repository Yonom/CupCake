using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public class PressGreenKeySendEvent : SendEvent
    {
        public PressGreenKeySendEvent(string encryption)
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