using System;
using CupCake.Players;

namespace CupCake.Permissions
{
    public static class PlayerExtensions
    {
        public static DateTime GetBanTimeout(this Player p)
        {
            DateTime t;
            p.Metadata.GetMetadata("BanTimeout", out t);
            return t;
        }

        public static void SetBanTimeout(this Player p, DateTime timeout)
        {
            p.Metadata.SetMetadata("BanTimeout", timeout);
        }

        public static string GetBanReason(this Player p)
        {
            string r;
            p.Metadata.GetMetadata("BanReason", out r);
            return r;
        }

        public static void SetBanReason(this Player p, string reason)
        {
            p.Metadata.SetMetadata("BanReason", reason);
        }

        public static Group GetGroup(this Player p)
        {
            Group g;
            p.Metadata.GetMetadata("Group", out g);
            return g;
        }

        public static void SetGroup(this Player p, Group group)
        {
            p.Metadata.SetMetadata("Group", group);
        }
    }
}