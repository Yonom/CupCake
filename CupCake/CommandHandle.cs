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
    internal class CommandHandle : CupCakeMuffinPart<object>, ICommand
    {
        public MethodBase Method { get; set; }

        public List<string> Labels { get; private set; }

        public List<string> Usages { get; private set; }

        public int MinArgs { get; set; }

        public Group MinGroup { get; set; }

        public bool HighPriority { get; set; }

        public Action<IInvokeSource, ParsedCommand> Callback { get; private set; }
        private string _name;

        public void Activate(Action<IInvokeSource, ParsedCommand> callback, string name)
        {
            this.Callback = callback;
            this._name = name;
            this.Method = callback.Method;

            this.Labels = new List<string>();
            this.Usages = new List<string>();

            // Alias attribute
            var highPriority =
                (HighPriorityAttribute)
                    this.Method.GetCustomAttributes(typeof(HighPriorityAttribute), false).FirstOrDefault();
            if (highPriority != null)
            {
                this.HighPriority = true;
            }

            // Alias attribute
            var labels = (LabelAttribute)this.Method.GetCustomAttributes(typeof(LabelAttribute), false).FirstOrDefault();
            if (labels != null)
            {
                this.Labels.AddRange(labels.Labels);
            }

            // MinGroup attribute
            var minGroup =
                (MinGroupAttribute)this.Method.GetCustomAttributes(typeof(MinGroupAttribute), false).FirstOrDefault();
            if (minGroup != null)
            {
                this.MinGroup = minGroup.Group;
            }

            // MinArgs attribute
            var minArgs =
                (MinArgsAttribute)this.Method.GetCustomAttributes(typeof(MinArgsAttribute), false).FirstOrDefault();
            if (minArgs != null)
            {
                this.MinArgs = minArgs.MinArgs;
            }

            // CorrectUsage attribute
            var correctUsage =
                (CorrectUsageAttribute[])this.Method.GetCustomAttributes(typeof(CorrectUsageAttribute), false);
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

       protected virtual bool CanHandle(ParsedCommand message)
        {
            return this.Labels.Any(l => l.Equals(message.Type, StringComparison.OrdinalIgnoreCase));
        }

       
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

        protected void Run(IInvokeSource source, ParsedCommand message)
        {
            this.Callback(source, message);
        }

        protected override void Enable()
        {
        }

        protected override string GetName()
        {
            return this._name;
        }
    }
}