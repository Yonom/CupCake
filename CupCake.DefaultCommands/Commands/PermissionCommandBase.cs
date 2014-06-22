using System;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.DefaultCommands.Commands
{
    public abstract class PermissionCommandBase : CommandBase<PermissionMuffin>
    {
        internal void RunPermissionCommand(IInvokeSource source, ParsedCommand message, Group permission)
        {
            string name = message.Args[0];
            this.PlayerService.MatchPlayer(name, player =>
            {
                this.RequireHigherRank(source, player);

                player.SetGroup(permission);

                source.Reply("{0} is now {1}.", player.ChatName, permission);
            }, username =>
            {
                string storageName = PlayerUtils.GetStorageName(username);
                this.RequireHigherRankOffline(source, storageName);
                this.Host.SetPermission(storageName, permission);
                source.Reply("{0} is now {1}.", PlayerUtils.GetChatName(username), permission);
            });
        }

        internal void RequireHigherRankOffline(IInvokeSource source, string name)
        {
            if (this.Host.GetPermission(name) >= source.Group)
                throw new CommandException(String.Format("You may not {0} a player with a higher rank.",
                    this.CommandName));
        }
    }
}