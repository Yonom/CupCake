using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Messages.Blocks;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class BlueCommand : UtilityCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Command("blue")]
        [CorrectUsage("")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            this.KeyService.PressKey(Key.Blue);
            source.Reply("Pressed blue key.");
        }
    }
}