using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Owner
{
    public sealed class LoadlevelCommand : OwnerCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Command("loadlevel")]
        [CorrectUsage("")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();
            this.Chatter.LoadLevel();
            source.Reply("Loaded level.");
        }
    }
}