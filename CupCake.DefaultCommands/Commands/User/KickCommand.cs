using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.User
{
    public class KickCommand : CommandBase<UserCommandsMuffin>
    {
        [MinArgs(1)]
        [MinGroup(Group.Trusted)]
        [Label("kick", "kickplayer")]
        [CorrectUsage("player [reason]")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();
            var player = this.PlayerService.MatchPlayer(message.Args[0]);
            this.RequireSameRank(source, player);

            this.Chatter.Kick(player.Username, message.GetTrail(1));

            source.Reply("Kicked {0}.", player.ChatName);
        }
    }
}