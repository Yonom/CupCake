using System;
using CupCake.Command.Source;
using JetBrains.Annotations;

namespace CupCake
{
    public static class InvokeSourceExtensions
    {
        [StringFormatMethod("message")]
        public static void Reply(this IInvokeSource invokeSource, string message, params object[] args)
        {
// ReSharper disable once RedundantStringFormatCall
            invokeSource.Reply(String.Format(message, args));
        }
    }
}