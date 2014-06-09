using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Permissions
{
    public class OpCommand : PermissionCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Admin)]
        [Label("op", "opplayer")]
        [CorrectUsage("player")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RunPermissionCommand(source, message, Group.Operator);
        }
    }
}
