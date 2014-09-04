using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class GiveWootCommand : UtilityCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Command("givewoot", "wootup")]
        [CorrectUsage("")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            this.ActionService.WootUp();
        }
    }
}