using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CupCake.API.Muffins;
using CupCake.Command.Attributes;
using CupCake.Command.Events;
using CupCake.Command.Source;
using CupCake.Core.Events;
using CupCake.Permissions;
using MuffinFramework;
using MuffinFramework.Muffins;
using MuffinFramework.Platforms;
using MuffinFramework.Services;

namespace CupCake.Command
{
    public abstract class CommandPart<TProtocol> : CupCakeMuffinPart<TProtocol>
    {
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
            var minGroup = (MinGroupAttribute)method.GetCustomAttributes(typeof(MinGroupAttribute), false).FirstOrDefault();
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

            this.Events.Bind<PlayerInvokeEvent>(this.OnPlayerInvoke, EventPriority.Lowest);
            this.Events.Bind<ExternalInvokeEvent>(this.OnExternalInvoke, EventPriority.Lowest);
        }

        public List<string> LabelsList { get; set; }

        public int MinArgs { get; set; }

        public Group MinGroup { get; set; }

        private void OnExternalInvoke(object sender, ExternalInvokeEvent e)
        {
            if (this.CanHandle(e.Message, e.Group))
            {
                this.ExcecuteCommand(e.Source, e.Message);
            }
        }

        private void OnPlayerInvoke(object sender, PlayerInvokeEvent e)
        {
            if (this.CanHandle(e.Message, e.Group))
            {
                this.ExcecuteCommand(new PlayerInvokeSource(e.Player, this.Chatter), e.Message);
            }
        }

        private bool CanHandle(string message, Group group)
        {
            // TODO: Implement this
            throw new NotSupportedException();
            return message.StartsWith("");
        }

        private void ExcecuteCommand(IInvokeSource source, string message)
        {
            this.Run(new InvokeArgs(source,message));
            source.Handled = true;
        }

        protected abstract void Run(InvokeArgs args);
    }
}
