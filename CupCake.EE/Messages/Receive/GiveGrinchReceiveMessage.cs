using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class GiveGrinchReceiveMessage : ReceiveMessage
    {
        //No arguments

        internal GiveGrinchReceiveMessage(Message message)
            : base(message)
        {
        }
    }
}