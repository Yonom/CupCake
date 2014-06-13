using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Owner
{
    public class KillAllCommand : OwnerCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Label("killall", "killemall")]
        [CorrectUsage("")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();
            this.Chatter.KillAll();
            source.Reply("Killed everyone.");
        }
    }
}
