using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CupCake.Command.Exceptions
{
    /// <summary>
    ///     Represents a general exception that happened while excecuting a command
    /// </summary>
    public class CommandException : Exception
    {
        public CommandException()
        {
        }

        public CommandException(string message)
            : base(message)
        {
        }

        public CommandException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}