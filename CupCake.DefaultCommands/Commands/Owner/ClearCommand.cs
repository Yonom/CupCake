using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Owner
{
    public sealed class ClearCommand : OwnerCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Command("clear")]
        [CorrectUsage("CLEAR")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();

            if (message.Count == 0 || message.Args[0] != "CLEAR")
                throw new CommandException("To use clear, type !clear CLEAR");

            this.RoomService.Clear();
            source.Reply("Cleared level.");
        }
    }
}