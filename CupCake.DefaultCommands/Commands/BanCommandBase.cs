using System;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.DefaultCommands.Commands
{
    public abstract class BanCommandBase : CommandBase<BanMuffinPart>
    {
        private void BanInternal(IInvokeSource source, string name, string reason, DateTime timeout)
        {
            PlayerService.MatchPlayer(name, player =>
            {
                RequireHigherRank(source, player);

                player.SetBanReason(reason);
                player.SetBanTimeout(timeout);
                player.SetGroup(Group.Banned);

                source.Reply("{0} is now Banned.", player.ChatName);
            }, username =>
            {
                string storageName = PlayerUtils.GetStorageName(username);
                RequireHigherRankOffline(source, storageName);
                Host.SetBanReason(storageName, reason);
                Host.SetBanTimeout(storageName, timeout);
                Host.SetPermission(storageName, Group.Banned);

                source.Reply("{0} is now Banned.", PlayerUtils.GetChatName(username));
            });
        }

        internal void Ban(IInvokeSource source, string name)
        {
            BanInternal(source, name, null, default(DateTime));
        }

        internal void Ban(IInvokeSource source, string name, string reason)
        {
            BanInternal(source, name, reason, default(DateTime));
        }

        internal void Ban(IInvokeSource source, string name, DateTime timeout)
        {
            BanInternal(source, name, null, timeout);
        }

        internal void Ban(IInvokeSource source, string name, DateTime timeout, string reason)
        {
            BanInternal(source, name, reason, timeout);
        }

        internal void RequireHigherRankOffline(IInvokeSource source, string name)
        {
            if (Host.GetPermission(name) >= source.Group)
                throw new CommandException(String.Format("You may not {0} a player with a higher rank.",
                    CommandName));
        }
    }
}