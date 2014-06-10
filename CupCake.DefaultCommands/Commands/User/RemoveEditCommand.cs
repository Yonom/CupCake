using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.User
{
    class RemoveEditCommand : UserCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Label("removeedit", "removeeditplayer")]
        [CorrectUsage("[player]")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();
            var player = this.GetPlayerOrSelf(source, message);
            this.RequireSameRank(source, player);

            this.Chatter.RemoveEdit(player.Username);

            source.Reply("Removed edit from {0}.", player.ChatName);
        }
    }
}
