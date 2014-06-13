using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    internal class HiCommand : UtilityCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Label("hi", "hello")]
        [CorrectUsage("[player]")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            source.Reply("Hello!");
        }
    }
}