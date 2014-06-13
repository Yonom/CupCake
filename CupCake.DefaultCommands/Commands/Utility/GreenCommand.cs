using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Messages.Blocks;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class GreenCommand : UtilityCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Label("green")]
        [CorrectUsage("")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.KeyService.PressKey(Key.Green);
            source.Reply("Pressed green key.");
        }
    }
}