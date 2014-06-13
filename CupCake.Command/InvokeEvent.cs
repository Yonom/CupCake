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

        /// <summary>
        ///     Gets or sets if commands should display a warning message, if the has already been handled.
        /// </summary>
        public bool IgnoresDuplicateWarning { get; set; }
    }
}