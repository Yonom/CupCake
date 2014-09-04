using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class PingCommand : UtilityCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Command("ping")]
        [CorrectUsage("")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            source.Reply("Pong.");
        }
    }
}