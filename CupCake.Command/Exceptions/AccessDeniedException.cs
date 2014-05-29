﻿using System;

namespace CupCake.Command.Exceptions
{
    /// <summary>
    ///     The exception that is thrown when a user that does not have the required rights attempts to run a command
    /// </summary>
    public class AccessDeniedException : Exception
    {
    }
}