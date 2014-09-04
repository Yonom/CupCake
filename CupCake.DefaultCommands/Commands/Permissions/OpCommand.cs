using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Permissions
{
    public sealed class OpCommand : PermissionCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Admin)]
        [Command("op", "opplayer")]
        [CorrectUsage("player")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RunPermissionCommand(source, message, Group.Operator);
        }
    }
}