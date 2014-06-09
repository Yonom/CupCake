using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Permissions
{
    public class AdminCommand : PermissionCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Host)]
        [Label("admin", "adminplayer")]
        [CorrectUsage("player")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RunPermissionCommand(source, message, Group.Admin);
        }
    }
}
