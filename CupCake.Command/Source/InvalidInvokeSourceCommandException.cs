using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CupCake.Command.Source
{
    public class InvalidInvokeSourceCommandException : CommandException 
    {
        public InvalidInvokeSourceCommandException()
        {
        }

        public InvalidInvokeSourceCommandException(string message)
            : base(message)
        {
        }

        public InvalidInvokeSourceCommandException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
