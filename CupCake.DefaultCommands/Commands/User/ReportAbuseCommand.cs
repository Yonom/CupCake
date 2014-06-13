using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.DefaultCommands.Commands.User
{
    public class ReportAbuseCommand : UserCommandBase
    {
        [MinArgs(2)]
        [MinGroup(Group.Host)]
        [Label("reportabuse", "reportabuseplayer")]
        [CorrectUsage("player reason")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            Player player = this.PlayerService.MatchPlayer(message.Args[0]);

            this.Chatter.ReportAbuse(player.Username, message.GetTrail(1));

            source.Reply("Reported {0}.", player.ChatName);
        }
    }
}