using System;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Messages.User;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.DefaultCommands.Commands
{
    public abstract class CommandBase<T> : Command<T>
    {
        internal virtual string CommandName
        {
            get { return Labels[0]; }
        }

        internal virtual void RequireOwner()
        {
            if (RoomService.AccessRight < AccessRight.Owner)
                throw new CommandException(String.Format("Bot must be world owner to be able to {0}.", CommandName));
        }

        internal virtual void RequireEdit()
        {
            if (RoomService.AccessRight < AccessRight.Edit)
                throw new CommandException(String.Format("Bot must have edit rights to be able to {0}.",
                    CommandName));
        }

        internal void RequireSameRank(IInvokeSource source, Player player)
        {
            if (player.GetGroup() > source.Group)
                throw new CommandException(String.Format("You may not {0} a player with a higher rank.",
                    CommandName));
        }

        internal void RequireHigherRank(IInvokeSource source, Player player)
        {
            if (player.GetGroup() >= source.Group)
                throw new CommandException(String.Format("You may not {0} a player with an equal or higher rank.",
                    CommandName));
        }
    }
}