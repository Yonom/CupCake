using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Messages.Blocks;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class CyanCommand : UtilityCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Command("cyan")]
        [CorrectUsage("")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            this.KeyService.PressKey(Key.Cyan);
            source.Reply("Pressed cyan key.");
        }
    }
}
