﻿using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class SayRawCommand : UtilityCommandBase
    {
        [MinArgs(1)]
        // Leave this to Admin, otherwise people can use !sayraw to gain access to higher ranks
        [MinGroup(Group.Admin)]
        [Command("sayraw")]
        [CorrectUsage("text")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            this.Chatter.ChatService.Send(message.GetTrail(0));
        }
    }
}