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
                this.Host.SetBanReason(username, reason);
                this.Host.SetBanTimeout(username, timeout);
                this.Host.SetPermission(username, Group.Banned);

                source.Reply("{0} is now banned.", PlayerUtils.GetChatName(username));
            });
        }

        public void Ban(IInvokeSource source, string name)
        {
            this.BanInternal(source, name, null, default(DateTime));
        }

        public void Ban(IInvokeSource source, string name, string reason)
        {
            this.BanInternal(source, name, reason, default(DateTime));
        }

        public void Ban(IInvokeSource source, string name, DateTime timeout)
        {
            this.BanInternal(source, name, null, timeout);
        }

        public void Ban(IInvokeSource source, string name, DateTime timeout, string reason)
        {
            this.BanInternal(source, name, reason, timeout);
        }
    }
}