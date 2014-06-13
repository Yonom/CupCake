using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Messages.Blocks;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class GetCrownCommand : UtilityCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Label("getcrown")]
        [CorrectUsage("")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.ActionService.GetCrown();
            source.Reply("Got crown.");
        }
    }
}
