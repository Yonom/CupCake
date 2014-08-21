using System;
using JetBrains.Annotations;

namespace CupCake.Command.Source
{
    public static class InvokeSourceExtensions
    {
        public static PlayerInvokeSource ToPlayerInvokeSource(this IInvokeSource source)
        {
            var playerSource = source as PlayerInvokeSource;
            if (playerSource == null)
                throw new InvalidInvokeSourceCommandException("You must call this command as a player.");

            return playerSource;
        }

        /// <summary>
        ///     Replies the specified chat message to the invoke source.
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