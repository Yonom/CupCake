using System;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Edit
{
    public sealed class GodModeCommand : EditCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Command("godmode")]
        [CorrectUsage("[enabled]")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireEdit();

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
                enabled = !this.PlayerService.OwnPlayer.IsGod;
            }

            this.ActionService.GodMode(enabled);

            source.Reply("God mode was set to {0}.", enabled);
        }
    }
}