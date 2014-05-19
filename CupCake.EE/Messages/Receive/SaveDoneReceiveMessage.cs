using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class SaveDoneReceiveMessage : ReceiveMessage
    {
        //No arguments

        public SaveDoneReceiveMessage(Message message)
            : base(message)
        {
        }
    }
}