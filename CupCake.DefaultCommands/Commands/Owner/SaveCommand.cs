using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Owner
{
    public sealed class SaveCommand : OwnerCommandBase
    {
        [MinGroup(Group.Operator)]
        [Label("save")]
        [CorrectUsage("SAVE")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();
            if (message.Count == 0 || message.Args[0] != "SAVE")
                throw new CommandException("To user clear, type !clear SAVE");

            this.RoomService.Save();
            source.Reply("Saved.");
        }
    }
}