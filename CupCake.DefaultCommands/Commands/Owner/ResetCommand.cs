using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Owner
{
    public sealed class ResetCommand : OwnerCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Label("reset")]
        [CorrectUsage("")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            RequireOwner();
            Chatter.Reset();
            source.Reply("Level reset.");
        }
    }
}