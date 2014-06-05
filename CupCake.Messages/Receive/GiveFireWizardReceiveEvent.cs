using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class GiveFireWizardReceiveEvent : ReceiveEvent
    {
        //No arguments

        public GiveFireWizardReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}