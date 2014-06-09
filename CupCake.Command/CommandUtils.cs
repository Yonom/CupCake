namespace CupCake.Command
{
    public static class CommandUtils
    {
        public static string TrimChatPrefix(string chatName)
        {
            if (chatName.StartsWith("~") || chatName.StartsWith("@"))
                return chatName.Substring(1);
            return chatName;
        }
    }
}
