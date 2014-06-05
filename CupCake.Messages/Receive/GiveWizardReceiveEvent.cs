using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class GiveWizardReceiveEvent : ReceiveEvent
    {
        //No arguments

        public GiveWizardReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}