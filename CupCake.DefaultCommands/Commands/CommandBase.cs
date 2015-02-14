using System;
using System.Linq;
using BotBits;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Messages.User;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.DefaultCommands.Commands
{
    public abstract class CommandBase<T> : PluginPart<T>
    {
        internal virtual string CommandName
        {
            get { return this.Commands.First().Labels[0]; }
        }

        protected override void Enable()
        {
        }

        internal virtual void RequireOwner()
        {
            if (this.Room.AccessRight < AccessRight.Owner)
                throw new CommandException(String.Format("Bot must be world owner to be able to {0}.", this.CommandName));
        }

        internal virtual void RequireEdit()
        {
            if (this.Room.AccessRight < AccessRight.Edit)
                throw new CommandException(String.Format("Bot must have edit rights to be able to {0}.",
                    this.CommandName));
        }

        internal void RequireSameRank(IInvokeSource source, Player player)
        {
            if (player.GetGroup() > source.Group)
                throw new CommandException(String.Format("You may not {0} a player with a higher rank.",
                    this.CommandName));
        }

        internal void RequireHigherRank(IInvokeSource source, Player player)
        {
            if (player.GetGroup() >= source.Group)
                throw new CommandException(String.Format("You may not {0} a player with an equal or higher rank.",
                    this.CommandName));
        }
    }
}