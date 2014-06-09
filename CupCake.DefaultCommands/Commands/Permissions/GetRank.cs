using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Permissions
{
    public class GetRank : CommandBase<PermissionMuffin>
    {
        [MinArgs(1)]
        [MinGroup(Group.Moderator)]
        [Label("getrank", "getplayerrank")]
        [CorrectUsage("player")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            var player = this.PlayerService.MatchPlayer(message.Args[0]);
            source.Reply("{0}'s rank is {1}", player.ChatName, player.GetGroup());
        }
    }
}
