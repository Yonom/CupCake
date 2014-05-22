using System.Globalization;

namespace CupCake.Chat
{
    public class BasicChatSyntaxProvider : IChatSyntaxProvider
    {
        public virtual string ApplyChatSyntax(string chat, string chatName)
        {
            return string.Format("[{0}] {1}", chatName, chat);
        }

        public virtual string ApplyReplySyntax(string chat, string chatName, string playerName)
        {
            return string.Format("[{0}] {1}: {2}", chatName, playerName.ToUpper(), chat);
        }

        public virtual string ApplyKickSyntax(string chatName, string playerName, string reason)
        {
            return string.Format("/kick {0} [{1}] {2}", playerName, chatName, reason);
        }
    }
}