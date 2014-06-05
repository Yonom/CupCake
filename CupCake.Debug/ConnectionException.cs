using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace CupCake.Debug
{
    public class ConnectionException : Exception
    {
        public ConnectionException(string message)
            : base(message)
        {
        }

        public ConnectionException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
