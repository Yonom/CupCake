using System;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Owner
{
    public sealed class EnablePotsCommand : OwnerCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Moderator)]
        [Label("enablepots")]
        [CorrectUsage("isAllowed")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();

            bool isAllowed;
            try
            {
                isAllowed = Boolean.Parse(message.Args[0]);
            }
            catch (Exception ex)
            {
                throw new CommandException("Unable to parse parameter: isAllowed", ex);
            }
            this.RoomService.SetAllowPotions(isAllowed);
            source.Reply("Potions enabled: {0}.", isAllowed);
        }
    }
}