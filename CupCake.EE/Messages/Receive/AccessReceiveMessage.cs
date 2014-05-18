using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class AccessReceiveMessage : ReceiveMessage
    {
        //No arguments

        internal AccessReceiveMessage(Message message)
            : base(message)
        {
        }
    }
}