using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Permissions
{
    public sealed class AdminCommand : PermissionCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Host)]
        [Command("admin", "adminplayer")]
        [CorrectUsage("player")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RunPermissionCommand(source, message, Group.Admin);
        }
    }
}