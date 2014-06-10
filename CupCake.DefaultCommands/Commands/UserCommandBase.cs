using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Players;

namespace CupCake.DefaultCommands.Commands
{
    public abstract class UserCommandBase : CommandBase<UserCommandsMuffin>
    {
        protected Player GetPlayerOrSelf(IInvokeSource source, ParsedCommand message)
        {
            if (message.Count >= 1)
            {
                return this.PlayerService.MatchPlayer(message.Args[0]);
            }
            var playerSource = source as PlayerInvokeSource;
            if (playerSource != null)
            {
                return playerSource.Player;
            }

            throw new UnknownPlayerCommandException("No player was specified!");
        }
    }
}
