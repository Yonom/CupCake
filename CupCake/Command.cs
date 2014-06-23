using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Core.Events;
using CupCake.Core.Log;
using CupCake.Permissions;

namespace CupCake
{
    /// <summary>
    /// Class Command.
    /// Represents a CupCake command.
    /// </summary>
    /// <typeparam name="TProtocol">The type of the protocol.</typeparam>
    public abstract class Command<TProtocol> : CupCakeMuffinPart<TProtocol>
    {
        /// <summary>
        /// Gets the labels list set for this command.
        /// Usually set through the <see cref="LabelAttribute"/>.
        /// </summary>
        /// <value>The labels.</value>
        public List<string> Labels { get; private set; }

        /// <summary>
        /// Gets the usages strings set for this command.
        /// Usually set through the <see cref="CorrectUsageAttribute"/>.
        /// </summary>
        /// <value>The usages.</value>
        public List<string> Usages { get; private set; }

        /// <summary>
        /// Gets or sets the minimum arguments.
        /// Usually set through the <see cref="MinArgsAttribute"/>.
        /// </summary>
        /// <value>The minimum arguments.</value>
        public int MinArgs { get; set; }

        /// <summary>
        /// Gets or sets the minimum permission required to execute this command.
        /// Usually set through the <see cref="MinGroupAttribute"/>.
        /// </summary>
        /// <value>The minimum group.</value>
        public Group MinGroup { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this command is executed with a higher priority.´
        /// Can only be set through the <see cref="HighPriorityAttribute"/> or the constructor, changing this value later has no effect.
        /// </summary>
        /// <value><c>true</c> if [high priority]; otherwise, <c>false</c>.</value>
        public bool HighPriority { get; set; }

        /// <summary>
        /// Enables this instance.
        /// Obsolete. You should not call Enable() on a command.
        /// </summary>
        [Obsolete("You should not call Enable() on a command.", true)]
#pragma warning disable 809
        protected override void Enable()
#pragma warning restore 809
        {
            this.Labels = new List<string>();
            this.Usages = new List<string>();

            MethodBase method = this.GetType().GetMethod("Run", BindingFlags.Instance | BindingFlags.NonPublic);

            // Alias attribute
            var highPriority =
                (HighPriorityAttribute)method.GetCustomAttributes(typeof(HighPriorityAttribute), false).FirstOrDefault();
            if (highPriority != null)
            {
                this.HighPriority = true;
            }

            // Alias attribute
            var labels = (LabelAttribute)method.GetCustomAttributes(typeof(LabelAttribute), false).FirstOrDefault();
            if (labels != null)
            {
                this.Labels.AddRange(labels.Labels);
            }

            // MinGroup attribute
            var minGroup =
                (MinGroupAttribute)method.GetCustomAttributes(typeof(MinGroupAttribute), false).FirstOrDefault();
            if (minGroup != null)
            {
                this.MinGroup = minGroup.Group;
            }

            // MinArgs attribute
            var minArgs = (MinArgsAttribute)method.GetCustomAttributes(typeof(MinArgsAttribute), false).FirstOrDefault();
            if (minArgs != null)
            {
                this.MinArgs = minArgs.MinArgs;
            }

            // CorrectUsage attribute
            var correctUsage = (CorrectUsageAttribute[])method.GetCustomAttributes(typeof(CorrectUsageAttribute), false);
            this.Usages.AddRange(correctUsage.Select(usage => usage.Usage));

            this.Events.Bind<InvokeEvent>(
                this.OnInvoke,
                this.HighPriority
                    ? EventPriority.Low
                    : EventPriority.Lowest);
        }

        private void OnInvoke(object sender, InvokeEvent e)
        {
            if (this.CanHandle(e.Message))
            {
                if (e.Handled)
                {
                    if (!e.IgnoresDuplicateWarning)
                    {
                        this.Logger.Log(LogPriority.Warning, "Detected possible duplicate command: " + e.Message.Type);
                    }
                }
                else
                {
                    e.Source.PluginName = this.GetName();
                    e.Handled = true;
                    this.ExecuteCommand(e.Source, e.Message);
                }
            }
        }

        /// <summary>
        /// Determines whether this instance can handle the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns><c>true</c> if this instance can handle the specified message; otherwise, <c>false</c>.</returns>
        protected virtual bool CanHandle(ParsedCommand message)
        {
            return this.Labels.Any(l => l.Equals(message.Type, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="source">The message source.</param>
        /// <param name="message">The parsed message.</param>
        protected void ExecuteCommand(IInvokeSource source, ParsedCommand message)
        {
            try
            {
                if (message is HelpRequest)
                {
                    source.Reply("Command usage: " + this.GetUsageStr(message.Type));
                    return;
                }

                if (source.Group < this.MinGroup)
                    throw new AccessDeniedException();
                if (message.Count < this.MinArgs)
                    throw new SyntaxException("Too few arguments.");

                this.Run(source, message);
            }
            catch (AccessDeniedException)
            {
                if (source.Group >= this.CommandService.ResponseMinGroup)
                    source.Reply("You are not allowed to run this command!");
            }
            catch (SyntaxException ex)
            {
                source.Reply(ex.Message + " Correct usage: " + this.GetUsageStr(message.Type));
            }
            catch (CommandException ex)
            {
                source.Reply("Error: " + ex.Message);
            }
        }

        private string GetUsageStr(string label)
        {
            string[] correctUsages =
                this.Usages.Select(usage => this.CommandService.CommandPrefix + label + " " + usage).ToArray();
            return correctUsages.Any()
                ? String.Join(" / ", correctUsages)
                : "<unavailable>";
        }

        /// <summary>
        /// This method is called whenever the command should be executed.
        /// </summary>
        /// <param name="source">The command source.</param>
        /// <param name="message">The parsed message.</param>
        protected abstract void Run(IInvokeSource source, ParsedCommand message);
    }
}