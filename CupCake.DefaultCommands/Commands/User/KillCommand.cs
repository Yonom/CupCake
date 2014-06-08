using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.DefaultCommands.Commands.User
{
    public class KillCommand : CommandBase<UserCommandsMuffin>
    {
        [MinArgs(1)]
        [MinGroup(Group.Moderator)]
        [Label("kill", "killplayer")]
        [CorrectUsage("player")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();
            var player = this.PlayerService.MatchPlayer(message.Args[0]);
            this.RequirePermissions(source, player);

            this.Chatter.Kill(player.Username);
        }
    }
}