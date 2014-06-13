using System;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Owner
{
    public sealed class VisibleCommand : OwnerCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Moderator)]
        [Label("visible")]
        [CorrectUsage("isVisible")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();

            bool isVisible;
            try
            {
                isVisible = Boolean.Parse(message.Args[0]);
            }
            catch (Exception ex)
            {
                throw new CommandException("Unable to parse parameter: isVisible", ex);
            }
            this.Chatter.ChangeVisibility(isVisible);
            source.Reply("Changed visibility to {0}.", isVisible);
        }
    }
}