using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CupCake.Command.Exceptions
{
    public class SyntaxException : Exception
    {
        public SyntaxException(string message)
            : base(message)
        {
        }
    }
}
