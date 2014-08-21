using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Core.Log;
using JetBrains.Annotations;

namespace CupCake
{
    /// <summary>
    /// Class LoggerExtensions.
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The object array that contains zero or more items to format.</param>
        [StringFormatMethod("message")]
        public static void Log(this Logger logger, string message, params object[] args)
        {
            // ReSharper disable once RedundantStringFormatCall
            logger.Log(String.Format(message, args));
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The object array that contains zero or more items to format.</param>
        [StringFormatMethod("message")]
        public static void Log(this Logger logger, LogPriority priority, string message, params object[] args)
        {
            // ReSharper disable once RedundantStringFormatCall
            logger.Log(priority, String.Format(message, args));
        }
    }
}
