using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    internal class DieCommand : UtilityCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Label("die")]
        [CorrectUsage("")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.ActionService.Die();
            source.Reply("Died.");
        }
    }
}