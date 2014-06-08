using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Ban
{
    public class TempBanCommand : CommandBase<BanMuffin>
    {
        [MinArgs(2)]
        [MinGroup(Group.Operator)]
        [Label("tempban", "tempbanplayer")]
        [CorrectUsage("player duration [reason]")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            var player = this.PlayerService.MatchPlayer(message.Args[0]);
            this.RequirePermissions(source, player);

            DateTime timeout;
            try
            {
                var duration = TimeSpan.Parse(message.Args[1]);
                timeout = DateTime.UtcNow.Add(duration);
            }
            catch (Exception ex)
            {
                throw new CommandException("Unable to parse duration!", ex);
            }


            if (message.Count >= 2)
            {
                Host.Ban(player, timeout, message.GetTrail(1));
            }
            else
            {
                Host.Ban(player, timeout);
            }
        }
    }
}
