using System;
using CupCake.Players;

namespace CupCake.Permissions
{
    public static class PlayerExtensions
    {
        public static DateTime GetBanTimeout(this Player p)
        {
            return p.Get<DateTime>("BanTimeout");
        }

        public static void SetBanTimeout(this Player p, DateTime timeout)
        {
            p.Set("BanTimeout", timeout);
        }

        public static string GetBanReason(this Player p)
        {
            return p.Get<string>("BanReason");
        }

        public static void SetBanReason(this Player p, string reason)
        {
            p.Set("BanReason", reason);
        }

        public static Group GetGroup(this Player p)
        {
            return p.Get<Group>("Group");
        }

        public static void SetGroup(this Player p, Group group)
        {
            p.Set("Group", group);
        }
    }
}