using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Owner
{
    public class LoadlevelCommand : OwnerCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Label("loadlevel")]
        [CorrectUsage("")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();
            this.Chatter.LoadLevel();
            source.Reply("Loaded level.");
        }
    }
}
