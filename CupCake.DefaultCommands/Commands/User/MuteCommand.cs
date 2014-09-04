using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.DefaultCommands.Commands.User
{
    public sealed class MuteCommand : UserCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Host)]
        [Command("mute", "muteplayer")]
        [CorrectUsage("player")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            Player player = this.PlayerService.MatchPlayer(message.Args[0]);

            this.Chatter.Mute(player.Username);

            source.Reply("Muted {0}.", player.ChatName);
        }
    }
}