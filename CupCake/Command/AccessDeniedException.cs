﻿using System;

namespace CupCake.Command
{
    /// <summary>
    ///     The exception that is thrown when a user that does not have the required rights attempts to run a command
    /// </summary>
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException()
        {
        }

        public AccessDeniedException(string message)
            : base(message)
        {
        }

        public AccessDeniedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}