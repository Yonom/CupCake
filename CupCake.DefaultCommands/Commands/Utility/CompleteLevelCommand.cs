using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    internal class CompleteLevelCommand : UtilityCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Label("completelevel")]
        [CorrectUsage("")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            if (this.PlayerService.OwnPlayer.HasSilverCrown)
                throw new CommandException("Bot already has crown!");
            this.ActionService.CompleteLevel();
        }
    }
}