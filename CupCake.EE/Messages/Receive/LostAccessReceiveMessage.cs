using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class LostAccessReceiveMessage : ReceiveMessage
    {
        //No arguments

        public LostAccessReceiveMessage(Message message)
            : base(message)
        {
        }
    }
}