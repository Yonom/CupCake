using System;
using CupCake.Players;

namespace CupCake.Command
{
    public static class CommandUtils
    {
        public static string TrimFilterPrefix(string chatName)
        {
            if (chatName.StartsWith("~") || chatName.StartsWith("@"))
                return chatName.Substring(1);
            return chatName;
        }
    }
}
