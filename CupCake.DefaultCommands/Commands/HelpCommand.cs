using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands
{
    class HelpCommand : CommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Moderator)]
        [Label("help")]
        [CorrectUsage("command")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.CommandService.Invoke(source, new HelpRequest(message.Args[0]));
        }
    }
}
