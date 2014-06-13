using System;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class AccessCommand : UtilityCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Label("access")]
        [CorrectUsage("[key]")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            string key = String.Empty;

            if (message.Count >= 1)
                key = message.GetTrail(0);

            this.RoomService.Access(key);

            source.Reply("Access sent.");
        }
    }
}