using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    class HiCommand : UtilityCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Label("hi", "hello")]
        [CorrectUsage("[player]")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            source.Reply("Hello!");
        }
    }
}
