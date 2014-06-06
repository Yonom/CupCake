using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.DefaultCommands.Commands
{
    public class KickCommand : Command<DefaultCommandsMuffin>
    {
        [MinArgs(1)]
        [MinGroup(Group.Limited)]
        [Label("kick", "kickplayer")]
        [CorrectUsage("player [reason]")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            Player player = this.PlayerService.MatchPlayer(message.Args[0]);

            if (player.GetGroup() > source.Group)
                throw new CommandException("You may not kick a player with a higher rank.");

            this.Chatter.Kick(player.Username, message.GetTrail(1));
        }
    }
}