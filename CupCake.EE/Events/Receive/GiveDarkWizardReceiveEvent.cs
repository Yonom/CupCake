using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public sealed class GiveDarkWizardReceiveEvent : ReceiveEvent
    {
        //No arguments

        public GiveDarkWizardReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}