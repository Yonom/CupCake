using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.User
{
    public class UnmuteCommand : UserCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Host)]
        [Label("unmute", "unmuteplayer")]
        [CorrectUsage("player")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            var player = this.PlayerService.MatchPlayer(message.Args[0]);

            this.Chatter.Unmute(player.Username);

            source.Reply("Unmuted {0}.", player.ChatName);
        }
    }
}
