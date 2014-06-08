using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CupCake.Core.Storage
{
    public class StorageException : Exception
    {
        public StorageException()
        {
            
        }

        public StorageException(string message)
            : base(message)
        {
            
        }

        public StorageException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
    }
}
