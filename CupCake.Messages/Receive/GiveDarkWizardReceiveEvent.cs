using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    /// Class Give Dark Wizard Receive Event.
    /// </summary>
    public class GiveDarkWizardReceiveEvent : ReceiveEvent
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GiveDarkWizardReceiveEvent"/> class.
        /// </summary>
        /// <param name="message">The EE message.</param>
        public GiveDarkWizardReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}