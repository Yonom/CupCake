using System;

namespace CupCake.Command
{
    public class SyntaxException : Exception
    {
        public SyntaxException(string message)
            : base(message)
        {
        }
    }
}