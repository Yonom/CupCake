using System;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class GuardianModeCommand : UtilityCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Label("guardianmode")]
        [CorrectUsage("[enabled]")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            bool enabled;

            if (message.Count >= 1)
            {
                try
                {
                    enabled = Boolean.Parse(message.Args[0]);
                }
                catch (Exception ex)
                {
                    throw new CommandException("Unable to parse parameter: enabled", ex);
                }
            }
            else
            {
                enabled = !this.PlayerService.OwnPlayer.IsGuardian;
            }

            this.ActionService.GuardianMode(enabled);

            source.Reply("Guardian mode was set to {0}.", enabled);
        }
    }
}