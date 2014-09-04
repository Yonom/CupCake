using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class HiCommand : UtilityCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Command("hi", "hello")]
        [CorrectUsage("")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            source.Reply("Hello!");
        }
    }
}