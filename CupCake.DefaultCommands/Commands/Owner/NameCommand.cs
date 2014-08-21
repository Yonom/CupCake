using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Owner
{
    public sealed class NameCommand : OwnerCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Moderator)]
        [Label("name")]
        [CorrectUsage("newName")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();

            string name = message.GetTrail(0);
            this.RoomService.SetName(name);
            source.Reply("Name changed to: {0}", name);
        }
    }
}