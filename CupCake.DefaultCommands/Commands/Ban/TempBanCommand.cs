using System;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Ban
{
    public sealed class TempBanCommand : BanCommandBase
    {
        [MinArgs(2)]
        [MinGroup(Group.Operator)]
        [Command("tempban", "tempbanplayer")]
        [CorrectUsage("player duration [reason]")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            DateTime timeout;
            try
            {
                TimeSpan duration = TimeSpan.Parse(message.Args[1]);
                timeout = DateTime.UtcNow.Add(duration);
            }
            catch (Exception ex)
            {
                throw new CommandException("Unable to parse parameter: duration", ex);
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