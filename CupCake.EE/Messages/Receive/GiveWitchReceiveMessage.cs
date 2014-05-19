using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class GiveWitchReceiveMessage : ReceiveMessage
    {
        //No arguments

        public GiveWitchReceiveMessage(Message message)
            : base(message)
        {
        }
    }
}