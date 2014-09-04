using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class ShutdownCommand : UtilityCommandBase
    {
        [MinGroup(Group.Admin)]
        [Command("shutdown", "end")]
        [CorrectUsage("")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            source.Reply("Shutting down...");

            this.HostService.Shutdown();
        }
    }
}