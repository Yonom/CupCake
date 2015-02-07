using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Messages.Blocks;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class YellowCommand : UtilityCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Command("yellow")]
        [CorrectUsage("")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            this.KeyService.PressKey(Key.Yellow);
            source.Reply("Pressed yellow key.");
        }
    }
}
