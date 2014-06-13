using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.DefaultCommands.Commands.Permissions
{
    public class GetRankCommand : PermissionCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Moderator)]
        [Label("getrank", "getplayerrank")]
        [CorrectUsage("player")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            var user = message.Args[0];
            this.PlayerService.MatchPlayer(user,
                player => 
                    source.Reply("{0}'s rank is {1}", player.ChatName, player.GetGroup()),
                username =>
                    source.Reply("{0} is now {1}.", PlayerUtils.GetChatName(username), this.Host.GetPermission(username)));
        }
    }
}
