using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.DefaultCommands.Commands
{
    public abstract class PermissionCommandBase : CommandBase<PermissionMuffin>
    {
        protected void RunPermissionCommand(IInvokeSource source, ParsedCommand message, Group permissions)
        {
            var username = message.Args[0];
            try
            {
                var player = this.PlayerService.MatchPlayer(username);
                this.RequireHigherRank(source, player);

                player.SetGroup(permissions);

                source.Reply("{0} is now {1}.", player.ChatName, permissions);
            }
            catch (UnknownPlayerCommandException)
            {
                username = CommandUtils.TrimChatPrefix(username);
                Host.SetPermission(username, permissions);

                source.Reply("{0} is now {1}.", PlayerUtils.GetChatName(username), permissions);
            }
        }
    }
}
