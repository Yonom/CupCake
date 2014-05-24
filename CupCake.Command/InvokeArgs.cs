using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command.Source;

namespace CupCake.Command
{
    public class InvokeArgs
    {
        public IInvokeSource Source { get; private set; }
        public string Message { get; private set; }

        public InvokeArgs(IInvokeSource source, string message)
        {
            this.Source = source;
            this.Message = message;
        }
    }
}
