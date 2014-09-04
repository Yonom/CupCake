using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.DefaultCommands.Commands.User
{
    public sealed class KickCommand : UserCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Trusted)]
        [Command("kick", "kickplayer")]
        [CorrectUsage("player [reason]")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();
            Player player = this.GetPlayerOrSelf(source, message);
            this.RequireSameRank(source, player);

            this.Chatter.ChatService.Kick(source.Name, player.Username,
                (message.Count > 1 ? message.GetTrail(1) : "Tsk tsk tsk"));
        }
    }
}