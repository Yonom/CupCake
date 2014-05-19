using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class GiveWizardReceiveMessage : ReceiveMessage
    {
        //No arguments

        public GiveWizardReceiveMessage(Message message)
            : base(message)
        {
        }
    }
}