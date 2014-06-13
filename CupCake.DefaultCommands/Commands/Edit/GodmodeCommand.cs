using System;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Edit
{
    public class GodModeCommand : EditCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Label("godmode")]
        [CorrectUsage("[enabled]")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireEdit();

            bool enabled = this.PlayerService.OwnPlayer.IsGod;

            if (message.Count >= 1)
                Boolean.TryParse(message.Args[0], out enabled);

            this.ActionService.GodMode(enabled);

            source.Reply("God mode was set to {0}.", enabled);
        }
    }
}