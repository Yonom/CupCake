using CupCake.Command.Source;

namespace CupCake.Command
{
    public class InvokeArgs
    {
        public InvokeArgs(IInvokeSource source, string message)
        {
            this.Source = source;
            this.Message = message;
        }

        public IInvokeSource Source { get; private set; }
        public string Message { get; private set; }
    }
}