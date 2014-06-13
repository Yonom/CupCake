using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Owner
{
    public sealed class SetKeyCommand : OwnerCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Host)]
        [Label("setkey")]
        [CorrectUsage("key")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();
            var key = message.GetTrail(0);
            this.RoomService.ChangeKey(key);
            source.Reply("Set key to {0}.", key);
        }
    }
}
