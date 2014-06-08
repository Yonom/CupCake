using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Ban
{
    class BanCommand : CommandBase<BanMuffin>
    {
        [MinArgs(1)]
        [MinGroup(Group.Operator)]
        [Label("ban", "banplayer")]
        [CorrectUsage("player [reason]")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            var player = this.PlayerService.MatchPlayer(message.Args[0]);
            this.RequirePermissions(source, player);

            if (message.Count >= 2)
            {
                this.Host.Ban(player, message.GetTrail(1));
            }
            else
            {
                this.Host.Ban(player);
            }
        }
    }
}
