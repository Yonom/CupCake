using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;

namespace CupCake
{
    /// <summary>
    /// Indicates that a method handles a command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class CommandAttribute : LabelAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandAttribute"/> class.
        /// </summary>
        /// <param name="labels">The labels for this command. Exclude the "!" character</param>
        public CommandAttribute(params string[] labels) : base(labels)
        {
            
        }
    }
}
