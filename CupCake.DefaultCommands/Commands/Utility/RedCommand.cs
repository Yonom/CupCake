using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Messages.Blocks;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    class RedCommand : UtilityCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Label("red")]
        [CorrectUsage("")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.KeyService.PressKey(Key.Red);
            source.Reply("Pressed red key.");
        }
    }
}
