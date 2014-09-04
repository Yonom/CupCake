using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class DieCommand : UtilityCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Command("die")]
        [CorrectUsage("")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            this.ActionService.Die();
            source.Reply("Died.");
        }
    }
}