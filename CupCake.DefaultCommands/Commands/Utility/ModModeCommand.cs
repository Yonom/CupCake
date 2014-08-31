using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class ModModeCommand : UtilityCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Label("modmode")]
        [CorrectUsage("")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.ActionService.ModMode();
            source.Reply("Entered mod mode.");
        }
    }
}