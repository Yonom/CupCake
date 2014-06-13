using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.DefaultCommands.Commands.User
{
    public sealed class KillCommand : UserCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Label("kill", "killplayer")]
        [CorrectUsage("[player]")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();
            Player player = this.GetPlayerOrSelf(source, message);
            this.RequireSameRank(source, player);

            this.Chatter.Kill(player.Username);

            source.Reply("Killed {0}.", player.ChatName);
        }
    }
}