using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake
{
    /// <summary>
    /// Interface ICommand.
    /// </summary>
    public interface ICommand : IDisposable
    {        
        /// <summary>
        ///     Gets or sets the method that is called when this command is invoked.
        /// </summary>
        /// <value>
        ///     The method that is called when this command is invoked.
        /// </value>
        MethodBase Method { get; set; }

        /// <summary>
        ///     Gets the labels list set for this command.
        ///     Usually set through the <see cref="LabelAttribute" />.
        /// </summary>
        /// <value>The labels.</value>
        List<string> Labels { get; }

        /// <summary>
        ///     Gets the usages strings set for this command.
        ///     Usually set through the <see cref="CorrectUsageAttribute" />.
        /// </summary>
        /// <value>The usages.</value>
        List<string> Usages { get; }

        /// <summary>
        ///     Gets or sets the minimum arguments.
        ///     Usually set through the <see cref="MinArgsAttribute" />.
        /// </summary>
        /// <value>The minimum arguments.</value>
        int MinArgs { get; set; }

        /// <summary>
        ///     Gets or sets the minimum permission required to execute this command.
        ///     Usually set through the <see cref="MinGroupAttribute" />.
        /// </summary>
        /// <value>The minimum group.</value>
        Group MinGroup { get; set; }

        /// <summary>
        ///     Gets a value indicating whether this command is executed with a higher priority.
        /// </summary>
        /// <value><c>true</c> if [high priority]; otherwise, <c>false</c>.</value>
        bool HighPriority { get; }

        /// <summary>
        /// Gets the callback that is called when this command is run.
        /// </summary>
        Action<IInvokeSource, ParsedCommand> Callback { get; }
    }
}
