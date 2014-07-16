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
            PlayerService.MatchPlayer(name, player =>
            {
                RequireHigherRank(source, player);

                player.SetGroup(permission);

                source.Reply("{0} is now {1}.", player.ChatName, permission);
            }, username =>
            {
                string storageName = PlayerUtils.GetStorageName(username);
                RequireHigherRankOffline(source, storageName);
                Host.SetPermission(storageName, permission);
                source.Reply("{0} is now {1}.", PlayerUtils.GetChatName(username), permission);
            });
        }

        internal void RequireHigherRankOffline(IInvokeSource source, string name)
        {
            if (Host.GetPermission(name) >= source.Group)
                throw new CommandException(String.Format("You may not {0} a player with a higher rank.",
                    CommandName));
        }
    }
}