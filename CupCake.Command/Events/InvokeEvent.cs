using CupCake.Command.Source;
using CupCake.Core.Events;
using CupCake.Permissions;

namespace CupCake.Command.Events
{
    public class InvokeEvent : Event
    {
        public InvokeEvent(IInvokeSource source, ParsedCommand message)
        {
            this.Source = source;
            this.Message = message;
        }

        public IInvokeSource Source { get; private set; }
        public ParsedCommand Message { get; set; }
        public bool Handled { get; set; }
    }
}