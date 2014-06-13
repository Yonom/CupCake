using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Owner
{
    public class ResetCommand : OwnerCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Label("reset")]
        [CorrectUsage("")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();
            this.Chatter.Reset() ;
            source.Reply("Level reset.");
        }
    }
}
