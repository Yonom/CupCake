using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Owner
{
    public sealed class SetKeyCommand : OwnerCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Host)]
        [Command("setkey")]
        [CorrectUsage("key")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();
            string key = message.GetTrail(0);
            this.RoomService.ChangeKey(key);
            source.Reply("Set key to {0}.", key);
        }
    }
}