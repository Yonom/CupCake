using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CupCake.Command
{
    public class UnknownPlayerCommandException : CommandException
    {
        public UnknownPlayerCommandException()
        {
        }

        public UnknownPlayerCommandException(string message)
            : base(message)
        {
        }

        public UnknownPlayerCommandException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
