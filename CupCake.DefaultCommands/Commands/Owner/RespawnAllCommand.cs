using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Owner
{
    class RespawnAllCommand : OwnerCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Label("respawnall")]
        [CorrectUsage("")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();
            this.Chatter.RespawnAll();
            source.Reply("Respawned all.");
        }
    }
}
