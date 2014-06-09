using System;
using CupCake.Players;

namespace CupCake.DefaultCommands
{
    public static class PlayerExtensions
    {
        public static bool GetRankLoaded(this Player p)
        {
            bool g;
            p.Metadata.GetMetadata("RankLoaded", out g);
            return g;
        }

        internal static void SetRankLoaded(this Player p, bool loaded)
        {
            p.Metadata.SetMetadata("RankLoaded", loaded);
        }
    }
}
