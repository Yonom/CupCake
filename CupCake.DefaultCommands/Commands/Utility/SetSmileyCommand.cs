using System;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Messages.User;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class SetSmileyCommand : UtilityCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Moderator)]
        [Label("setsmiley", "setface")]
        [CorrectUsage("smiley")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            Smiley smiley;
            try
            {
                smiley = (Smiley)Enum.Parse(typeof(Smiley), message.Args[0], true);
            }
            catch (Exception ex)
            {
                throw new CommandException("Unable to parse parameter: smiley", ex);
            }

            this.ActionService.ChangeFace(smiley);

            source.Reply("Smiley set to {0}.", smiley);
        }
    }
}