using BotBits;

namespace CupCake.Command
{
    internal static class PlayerExtensions
    {
        internal static string GetTrimmedName(this Player p)
        {
            var g = p.Get<string>("TrimmedName");
            if (g == null)
            {
                g = ChatUtils.ApplyAntiSpam(p.Username);
                p.SetTrimmedName(g);
            }
            return g;
        }

        private static void SetTrimmedName(this Player p, string name)
        {
            p.Set("TrimmedName", name);
        }
    }
}