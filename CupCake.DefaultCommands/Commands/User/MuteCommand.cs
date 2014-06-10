using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.User
{
    public class MuteCommand : UserCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Host)]
        [Label("mute", "muteplayer")]
        [CorrectUsage("player")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            var player = this.PlayerService.MatchPlayer(message.Args[0]);

            this.Chatter.Mute(player.Username);

            source.Reply("Muted {0}.", player.ChatName);
        }
    }
}
