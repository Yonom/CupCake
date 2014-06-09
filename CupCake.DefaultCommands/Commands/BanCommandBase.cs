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
    public abstract class BanCommandBase : CommandBase<BanMuffinPart>
    {
        public void Ban(IInvokeSource source, string name)
        {
            try
            {
                var player = this.PlayerService.MatchPlayer(name);
                this.RequireHigherRank(source, player);

                player.SetGroup(Group.Banned);

                source.Reply("{0} is now banned.", player.ChatName);
            }
            catch (UnknownPlayerCommandException)
            {
                name = CommandUtils.TrimChatPrefix(name);
                Host.SetPermission(name, Group.Banned);

                source.Reply("{0} is now banned.", PlayerUtils.GetChatName(name));
            }
        }

        public void Ban(IInvokeSource source, string name, string reason)
        {
            Host.SetBanReason(name, reason);
            this.Ban(source, name);
        }

        public void Ban(IInvokeSource source, string name, DateTime timeout)
        {
            Host.SetBanTimeout(name, timeout);
            this.Ban(source, name);
        }

        public void Ban(IInvokeSource source, string name, DateTime timeout, string reason)
        {
            Host.SetBanTimeout(name, timeout);
            Host.SetBanReason(name, reason);
            this.Ban(source, name);
        }
    }
}
