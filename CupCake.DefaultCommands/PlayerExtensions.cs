using System;
using CupCake.Players;

namespace CupCake.DefaultCommands
{
    public static class PlayerExtensions
    {
        public static bool HasBanReason(this Player p)
        {
            string g;
            return p.Metadata.GetMetadata("BanReason", out g);
        }

        public static string GetBanReason(this Player p)
        {
            string g;
            p.Metadata.GetMetadata("BanReason", out g);
            return g;
        }

        internal static void SetBanReason(this Player p, string reason)
        {
            p.Metadata.SetMetadata("BanReason", reason);
        }


        public static bool HasBanTimeout(this Player p)
        {
            DateTime g;
            return p.Metadata.GetMetadata("BanTimeout", out g);
        }

        public static DateTime GetBanTimeout(this Player p)
        {
            DateTime g;
            p.Metadata.GetMetadata("BanTimeout", out g);
            return g;
        }

        internal static void SetBanTimeout(this Player p, DateTime timeout)
        {
            p.Metadata.SetMetadata("BanTimeout", timeout);
        }


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
