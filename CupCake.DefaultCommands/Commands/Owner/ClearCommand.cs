using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Owner
{
    public class ClearCommand : OwnerCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Label("clear")]
        [CorrectUsage("CLEAR")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();

            if (message.Count == 0 || message.Args[0] != "CLEAR")
                throw new CommandException("To user clear, type !clear CLEAR");

            this.RoomService.Clear();
            source.Reply("Cleared level.");
        }
    }
}
