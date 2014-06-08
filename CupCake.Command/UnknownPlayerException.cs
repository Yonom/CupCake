using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CupCake.Command
{
    public class UnknownPlayerException : CommandException
    {
        public UnknownPlayerException()
        {
        }

        public UnknownPlayerException(string message)
            : base(message)
        {
        }

        public UnknownPlayerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
