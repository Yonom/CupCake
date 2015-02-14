using System;
using System.Collections.Generic;
using BotBits;

namespace CupCake.Command
{
    public static class PlayerServiceExtensions
    {
        public static Player MatchPlayer(this Players playerService, string filter) // TODO: refactor this
        {
            if (filter.Length >= 2)
            {
                bool firstResult = filter.StartsWith("~");
                bool exactMatch = filter.StartsWith("@");

                // Wild card matching requested
                // Remove the wild cards from the string
                filter = CommandUtils.TrimFilterPrefix(filter);

                IList<Player> list = new List<Player>();

                // Match based on the positioning of the wild card
                foreach (Player player in playerService)
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


        public static void MatchPlayer(this Players playerService, string filter, Action<Player> onlineCallback,
            Action<string> offlineCallback) // TODO: refactor this
        {
            Player player;
            try
            {
                player = playerService.MatchPlayer(filter);
            }
            catch (UnknownPlayerCommandException)
            {
                string username = CommandUtils.TrimFilterPrefix(filter);
                offlineCallback(username);
                return;
            }

            onlineCallback(player);
        }
    }
}