using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class GiveFireWizardReceiveMessage : ReceiveMessage
    {
        //No arguments

        public GiveFireWizardReceiveMessage(Message message)
            : base(message)
        {
        }
    }
}