using PlayerIOClient;

namespace CupCake.EE.Events.Receive
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