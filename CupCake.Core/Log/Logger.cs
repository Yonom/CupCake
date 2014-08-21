using System;
using JetBrains.Annotations;

namespace CupCake.Core.Log
{
    public class Logger
    {
        public Logger(LogPlatform logPlatform, string name)
        {
            this.LogPlatform = logPlatform;
            this.Name = name;
        }

        public LogPlatform LogPlatform { get; private set; }
        public string Name { get; set; }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Log(string message)
        {
            this.Log(LogPriority.Message, message);
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="priority">The priority.</param>
        /// <param name="message">The message.</param>
        public void Log(LogPriority priority, string message)
        {
            this.LogPlatform.Log(this.Name, priority, message);
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The object array that contains zero or more items to format.</param>
        [StringFormatMethod("message")]
        public void Log(string message, params object[] args)
        {
            // ReSharper disable once RedundantStringFormatCall
            this.Log(String.Format(message, args));
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="priority">The priority.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The object array that contains zero or more items to format.</param>
        [StringFormatMethod("message")]
        public void Log(LogPriority priority, string message, params object[] args)
        {
            // ReSharper disable once RedundantStringFormatCall
            this.Log(priority, String.Format(message, args));
        }
    }
}