using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.User
{
    public class GiveEditCommand : UserCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Label("giveedit", "giveeditplayer")]
        [CorrectUsage("[player]")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();
            var player = this.GetPlayerOrSelf(source, message);
            this.RequireSameRank(source, player);

            this.Chatter.GiveEdit(player.Username);

            source.Reply("Gave edit to {0}.", player.ChatName);
        }
    }
}
