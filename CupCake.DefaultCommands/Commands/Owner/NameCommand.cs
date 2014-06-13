using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Owner
{
    public class NameCommand : OwnerCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Moderator)]
        [Label("name")]
        [CorrectUsage("newName")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();
            this.RoomService.SetName(message.GetTrail(0));
            source.Reply("Killed everyone.");
        }
    }
}
