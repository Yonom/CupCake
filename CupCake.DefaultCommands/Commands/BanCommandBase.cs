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
            this.PlayerService.MatchPlayer(name, player =>
            {
                this.RequireHigherRank(source, player);

                player.SetBanReason(reason);
                player.SetBanTimeout(timeout);
                player.SetGroup(Group.Banned);

                source.Reply("{0} is now banned.", player.ChatName);
            }, username =>
            {
                var storageName = PlayerUtils.GetStorageName(username);
                this.RequireHigherRankOffline(source, storageName);
                this.Host.SetBanReason(storageName, reason);
                this.Host.SetBanTimeout(storageName, timeout);
                this.Host.SetPermission(storageName, Group.Banned);

                source.Reply("{0} is now banned.", PlayerUtils.GetChatName(username));
            });
        }

        internal void Ban(IInvokeSource source, string name)
        {
            this.BanInternal(source, name, null, default(DateTime));
        }

        internal void Ban(IInvokeSource source, string name, string reason)
        {
            this.BanInternal(source, name, reason, default(DateTime));
        }

        internal void Ban(IInvokeSource source, string name, DateTime timeout)
        {
            this.BanInternal(source, name, null, timeout);
        }

        internal void Ban(IInvokeSource source, string name, DateTime timeout, string reason)
        {
            this.BanInternal(source, name, reason, timeout);
        }

        internal void RequireHigherRankOffline(IInvokeSource source, string name)
        {
            if (this.Host.GetPermission(name) >= source.Group)
                throw new CommandException(String.Format("You may not {0} a player with a higher rank.",
                    this.CommandName));
        }
    }
}