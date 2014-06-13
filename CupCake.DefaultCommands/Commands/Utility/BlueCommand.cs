using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Messages.Blocks;
using CupCake.Messages.User;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class BlueCommand : UtilityCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Label("blue")]
        [CorrectUsage("")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.KeyService.PressKey(Key.Blue);
            source.Reply("Pressed blue key.");
        }
    }
}
