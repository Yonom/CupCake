using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class SayRawCommand : CommandBase<UtilityCommandsMuffin>
    {
        [MinArgs(1)]
        // Leave this to Admin, otherwise people can use !sayraw to gain access to higher ranks
        [MinGroup(Group.Admin)]
        [Label("sayraw")]
        [CorrectUsage("text")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.Chatter.ChatService.Send(message.GetTrail(0));
        }
    }
}
