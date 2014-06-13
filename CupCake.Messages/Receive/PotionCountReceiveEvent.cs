using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class PotionCountReceiveEvent : ReceiveEvent
    {
        //No arguments

        public PotionCountReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}