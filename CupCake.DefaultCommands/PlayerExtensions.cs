using CupCake.Players;

namespace CupCake.DefaultCommands
{
    public static class PlayerExtensions
    {
        public static bool GetRankLoaded(this Player p)
        {
            return p.Get<bool>("RankLoaded");
        }

        internal static void SetRankLoaded(this Player p, bool loaded)
        {
            p.Set("RankLoaded", loaded);
        }
    }
}