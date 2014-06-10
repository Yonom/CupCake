using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class PingCommand : CommandBase<UtilityCommandsMuffin>
    {
        [MinGroup(Group.Moderator)]
        [Label("ping")]
        [CorrectUsage("")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            source.Reply("Pong.");
        }
    }
}
