using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public class GiveDarkWizardReceiveEvent : ReceiveEvent
    {
        //No arguments

        public GiveDarkWizardReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}