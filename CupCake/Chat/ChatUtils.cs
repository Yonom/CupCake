using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CupCake
{
    public static class ChatUtils
    {
        public static string ApplyAntiSpam(string input)
        {
            input = input.Trim();

            bool isCommand = input.StartsWith("/");

            if (!isCommand)
            {
                input = Regex.Replace(input, @"([\?\!]{2})[\?\!]+", "$1",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
                input = Regex.Replace(input, @"\.{4,}", "...", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

                bool replaced = true;
                while (replaced)
                {
                    replaced = false;
                    input = Regex.Replace(input, @"(.+?)\1{4,}", m =>
                    {
                        replaced = true;
                        return m.Groups[1].ToString() + m.Groups[1] + m.Groups[1];
                    }, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
                }

                if (input.Length > 4 && input.Count(Char.IsUpper) > input.Length / 2)
                    input = input.ToLower();
            }

            return input;
        }
    }
}