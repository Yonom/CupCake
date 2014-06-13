using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Permissions
{
    public class ModCommand : PermissionCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Operator)]
        [Label("mod", "modplayer")]
        [CorrectUsage("player")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RunPermissionCommand(source, message, Group.Moderator);
        }
    }
}