using System;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Ban
{
    public class TempBanCommand : BanCommandBase
    {
        [MinArgs(2)]
        [MinGroup(Group.Operator)]
        [Label("tempban", "tempbanplayer")]
        [CorrectUsage("player duration [reason]")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            DateTime timeout;
            try
            {
                var duration = TimeSpan.Parse(message.Args[1]);
                timeout = DateTime.UtcNow.Add(duration);
            }
            catch (Exception ex)
            {
                throw new CommandException("Unable to parse duration!", ex);
            }

            if (message.Count >= 3)
            {
                this.Ban(source, message.Args[0], timeout, message.GetTrail(2));
            }
            else
            {
                this.Ban(source, message.Args[0], timeout);
            }
        }
    }
}
