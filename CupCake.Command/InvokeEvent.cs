using CupCake.Command.Source;
using CupCake.Core.Events;

namespace CupCake.Command
{
    public class InvokeEvent : Event
    {
        public InvokeEvent(IInvokeSource source, ParsedCommand message)
        {
            this.Source = source;
            this.Message = message;
        }

        public IInvokeSource Source { get; set; }
        public ParsedCommand Message { get; set; }
        public bool Handled { get; set; }
    }
}