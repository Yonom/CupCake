using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class GiveGrinchReceiveMessage : ReceiveMessage
    {
        //No arguments

        public GiveGrinchReceiveMessage(Message message)
            : base(message)
        {
        }
    }
}