using System;
using CupCake.Command.Source;
using JetBrains.Annotations;

namespace CupCake
{
    /// <summary>
    /// Class InvokeSourceExtensions.
    /// </summary>
    public static class InvokeSourceExtensions
    {
        /// <summary>
        /// Replies the specified chat message to the invoke source.
        /// </summary>
        /// <param name="invokeSource">The invoke source.</param>
        /// <param name="message">The chat message.</param>
        /// <param name="args">The object array that contains zero or more items to format.</param>
        [StringFormatMethod("message")]
        public static void Reply(this IInvokeSource invokeSource, string message, params object[] args)
        {
// ReSharper disable once RedundantStringFormatCall
            invokeSource.Reply(String.Format(message, args));
        }
    }
}