using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public sealed class GiveWizardReceiveEvent : ReceiveEvent
    {
        //No arguments

        public GiveWizardReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}