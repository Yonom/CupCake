using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class ShutdownCommand : UtilityCommandBase
    {
        [MinGroup(Group.Admin)]
        [Label("shutdown", "end")]
        [CorrectUsage("")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            source.Reply("Shutting down...");

            this.HostService.Shutdown();
        }
    }
}