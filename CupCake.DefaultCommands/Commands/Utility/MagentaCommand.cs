using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Messages.Blocks;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class MagentaCommand : UtilityCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Command("magenta")]
        [CorrectUsage("")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            this.KeyService.PressKey(Key.Magenta);
            source.Reply("Pressed magenta key.");
        }
    }
}
