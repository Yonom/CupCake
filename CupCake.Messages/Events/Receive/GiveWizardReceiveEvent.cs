using PlayerIOClient;

namespace CupCake.Messages.Events.Receive
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