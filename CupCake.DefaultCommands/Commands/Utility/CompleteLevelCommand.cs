using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class CompleteLevelCommand : UtilityCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Command("completelevel")]
        [CorrectUsage("")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            if (this.PlayerService.OwnPlayer.HasSilverCrown)
                throw new CommandException("Bot already has crown!");
            this.ActionService.CompleteLevel();
        }
    }
}