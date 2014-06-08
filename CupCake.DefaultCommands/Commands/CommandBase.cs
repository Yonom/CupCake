using System;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Messages.User;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.DefaultCommands.Commands
{
    public abstract class CommandBase : Command<DefaultCommandsMuffin>
    {
        protected virtual void RequireOwner()
        {
            var commandName = this.Labels[0];

            if (this.RoomService.AccessRight < AccessRight.Owner)
                throw new CommandException(String.Format("Bot must be world owner to be able to {0}.", commandName));
        }

        protected void RequirePermissions(IInvokeSource source, Player player)
        {
            var commandName = this.Labels[0];
            if (player.GetGroup() > source.Group)
                throw new CommandException(String.Format("You may not {0} a player with a higher rank.", commandName));
        }
    }
}
