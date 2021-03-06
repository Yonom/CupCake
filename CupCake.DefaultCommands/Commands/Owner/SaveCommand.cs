﻿using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Owner
{
    public sealed class SaveCommand : OwnerCommandBase
    {
        [MinGroup(Group.Operator)]
        [Command("save")]
        [CorrectUsage("SAVE")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();
            if (message.Count == 0 || message.Args[0] != "SAVE")
                throw new CommandException("To use save, type !save SAVE");

            this.RoomService.Save();
            source.Reply("Saved.");
        }
    }
}