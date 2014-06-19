using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Permissions
{
    public sealed class UserCommand : PermissionCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Operator)]
        [Label("user", "userplayer",
            "unban", "unlimit", "untrust", "unmod", "unop", "unadmin",
            "unbanplayer", "unlimitplayer", "untrustplayer", "unmodplayer", "unopplayer", "unadminplayer")]
        [CorrectUsage("player")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RunPermissionCommand(source, message, Group.User);
        }
    }
}