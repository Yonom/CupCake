using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class SayCommand : CommandBase<UtilityCommandsMuffin>
    {
        [MinArgs(1)]
        [MinGroup(Group.Moderator)]
        [Label("say")]
        [CorrectUsage("text")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.Chatter.ChatService.Chat(message.GetTrail(0), source.Name);
        }
    }
}
