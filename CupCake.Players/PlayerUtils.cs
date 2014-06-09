using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CupCake.Players
{
    public static class PlayerUtils
    {
        public static bool IsGuest(string username)
        {
            // Offcial implmentation in SWF, don't blame me
            return username.Contains("-");
        }

        public static string GetStorageName(string username)
        {
            if (IsGuest(username))
                return "guest";
            return username.ToLowerInvariant();
        }

        public static string GetChatName(string username)
        {
            return username.ToUpperInvariant();
        }
    }
}
