using System;
using System.Collections.Generic;
using CupCake.Chat;
using CupCake.Players;

namespace CupCake.Command
{
    public static class PlayerServiceExtensions
    {
        public static Player MatchPlayer(this PlayerService playerService, string filter)
        {
            if (filter.Length >= 2)
            {
                bool firstResult = filter.StartsWith("~");
                bool exactMatch = filter.StartsWith("@");

                // Wild card matching requested
                // Remove the wild cards from the string
                filter = CommandUtils.TrimChatPrefix(filter);

                IList<Player> list = new List<Player>();

                // Match based on the positioning of the wild card
                foreach (Player player in playerService.Players)
                {
                    if (exactMatch)
                    {
                        if (player.Username.Equals(filter, StringComparison.OrdinalIgnoreCase))
                        {
                            return player;
                        }
                    }
                    else
                    {
                        if (player.Username.StartsWith(filter, StringComparison.OrdinalIgnoreCase) || 
                            player.GetTrimmedName().StartsWith(filter, StringComparison.OrdinalIgnoreCase))
                        {
                            list.Add(player);
                        }
                    }
                }

                if (list.Count == 0)
                    throw new UnknownPlayerCommandException("No player found!");
                if (!firstResult && list.Count >= 2)
                    throw new CommandException("More than one player was found.");

                return list[0];
            }

            throw new CommandException("Player query was too short. Be more specific!");
        }
    }
}