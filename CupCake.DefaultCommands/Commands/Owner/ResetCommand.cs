﻿using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Owner
{
    public sealed class ResetCommand : OwnerCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Command("reset")]
        [CorrectUsage("")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();
            this.Chatter.Reset();
            source.Reply("Level reset.");
        }
    }
}