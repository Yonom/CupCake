using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CupCake.Command;
using CupCake.Command.Attributes;
using CupCake.Command.Events;
using CupCake.Command.Exceptions;
using CupCake.Command.Source;
using CupCake.Core.Events;
using CupCake.Permissions;

namespace CupCake.Muffins
{
    public abstract class Command<TProtocol> : CupCakeMuffinPart<TProtocol>
    {
        public List<string> LabelsList { get; set; }

        public int MinArgs { get; set; }

        public Group MinGroup { get; set; }

        public string Usage { get; set; }

        protected override void Enable()
        {
            MethodBase method = this.GetType().GetMethod("Run", BindingFlags.Instance | BindingFlags.Public);

            // Alias attribute
            var labels = (LabelAttribute)method.GetCustomAttributes(typeof(LabelAttribute), false).FirstOrDefault();
            if (labels != null)
            {
                this.LabelsList.AddRange(labels.Labels);
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
            var correctUsage =
                (CorrectUsageAttribute)method.GetCustomAttributes(typeof(CorrectUsageAttribute), false).FirstOrDefault();
            if (correctUsage != null)
            {
                this.Usage = correctUsage.Usage;
            }

            this.Events.Bind<PlayerInvokeEvent>(this.OnPlayerInvoke, EventPriority.Lowest);
            this.Events.Bind<InvokeEvent>(this.OnInvoke, EventPriority.Lowest);
        }

        private void OnInvoke(object sender, InvokeEvent e)
        {
            if (this.CanHandle(e.Message))
            {
                this.ExcecuteCommand(e.Source, e.Message);
                e.Handled = true;
            }
        }

        private void OnPlayerInvoke(object sender, PlayerInvokeEvent e)
        {
            if (this.CanHandle(e.Message))
            {
                this.ExcecuteCommand(new PlayerInvokeSource(sender, e.Group, e.Player, this.Chatter), e.Message);
                e.Handled = true;
            }
        }

        protected virtual bool CanHandle(ParsedCommand message)
        {
            return this.LabelsList.Any(l => l.Equals(message.Type, StringComparison.OrdinalIgnoreCase));
        }

        protected void ExcecuteCommand(IInvokeSource source, ParsedCommand message)
        {
            try
            {
                if (source.Group < this.MinGroup)
                    throw new AccessDeniedException();
                if (message.Count < this.MinArgs)
                    throw new SyntaxException("Too few arguments.");

                this.Run(source, message);
            }
            catch (AccessDeniedException)
            {
                source.Reply("You are not allowed to run this command!");
            }
            catch (SyntaxException ex)
            {
                source.Reply("Error excecuting command: " + ex.Message + "\nCorrect usage: !command " + this.Usage);
            }
            catch (CommandException ex)
            {
                source.Reply("Error excecuting command: " + ex.Message);
            }
        }

        protected abstract void Run(IInvokeSource source, ParsedCommand message);
    }
}