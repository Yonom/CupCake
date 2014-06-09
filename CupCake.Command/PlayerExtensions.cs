using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Chat;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.Command
{
    internal static class PlayerExtensions
    {
        internal static string GetTrimmedName(this Player p)
        {
            string g;
            p.Metadata.GetMetadata("TrimmedName", out g);
            if (g == null)
            {
                g = ChatUtils.ApplyAntiSpam(p.Username);
                p.SetTrimmedName(g);
            }
            return g;
        }

        private static void SetTrimmedName(this Player p, string name)
        {
            p.Metadata.SetMetadata("TrimmedName", name);
        }
    }
}
