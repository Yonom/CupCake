using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.DefaultCommands.Commands.User
{
    public class KickCommand : UserCommandBase
    {
        [MinGroup(Group.Trusted)]
        [Label("kick", "kickplayer")]
        [CorrectUsage("[player] [reason]")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();
            Player player = this.GetPlayerOrSelf(source, message);
            this.RequireSameRank(source, player);

            this.Chatter.Kick(player.Username, message.GetTrail(1));
        }
    }
}