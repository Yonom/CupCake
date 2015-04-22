﻿using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.DefaultCommands.Commands.Permissions
{
    public sealed class GetRankCommand : PermissionCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Command("getrank", "getplayerrank")]
        [CorrectUsage("[player]")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            if (message.Count >= 1)
            {
                string user = message.Args[0];
                this.PlayerService.MatchPlayer(user,
                    player =>
                        source.Reply("{0}'s rank is {1}", player.ChatName, player.GetGroup()),
                    username =>
                        source.Reply("{0} is now {1}.", PlayerUtils.GetChatName(username),
                            this.Host.GetPermission(PlayerUtils.GetStorageName(username))));
                return;
            }
            var playerSource = source as PlayerInvokeSource;
            if (playerSource != null)
            {
                source.Reply("{0}'s rank is {1}", playerSource.Player.ChatName, playerSource.Player.GetGroup());
            }
            else
            {
                throw new UnknownPlayerCommandException("No player was specified!");
            }
        }
    }
}