using System;
using System.Collections.Generic;
using CupCake.Players;

namespace CupCake.Command
{
    public static class PlayerParser
    {
        public static Player MatchPlayer(this PlayerService playerService, string filter)
        {
            if (filter.Length >= 2)
            {
                bool exactMatch = filter.StartsWith("@");

                // Wild card matching requested
                // Remove the wild cards from the string
                if (exactMatch)
                {
                    filter = filter.Substring(1);
                }

                IList<Player> list = new List<Player>();

                // Match based on the positioning of the wild card
                foreach (var player in playerService.Players)
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
                        if (player.Username.StartsWith(filter, StringComparison.OrdinalIgnoreCase))
                        {
                            list.Add(player);
                        }
                    }
                }

                if (list.Count == 0)
                    throw new CommandException("No player found!");
                if (list.Count >= 2)
                    throw new CommandException("More than one player was found.");

                return list[0];
            }

            throw new CommandException("Player query was too short. Be more specific!");
        }
    }
}