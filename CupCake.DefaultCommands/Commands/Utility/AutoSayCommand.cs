using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Messages.User;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class AutoSayCommand : UtilityCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Moderator)]
        [Label("autosay")]
        [CorrectUsage("text")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            AutoText text;
            try
            {
                text = (AutoText)Enum.Parse(typeof(AutoText), message.Args[0], true);
            }
            catch (Exception ex)
            {
                throw new CommandException("Unable to parse parameter: text", ex);
            }

            this.ActionService.AutoSay(text);
        }
    }
}
