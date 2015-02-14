namespace CupCake
{
    public class BasicChatSyntaxProvider : IChatSyntaxProvider
    {
        public virtual string ApplyChatSyntax(string chat, string chatName)
        {
            return string.Format("[{0}] {1}", chatName, chat);
        }

        public virtual string ApplyReplySyntax(string chat, string playerName, string chatName)
        {
            return string.Format("[{0}] {1}: {2}", chatName, playerName.ToUpper(), chat);
        }

        public string ApplyPrivateMessageSyntax(string playerName, string chat, string chatName)
        {
            return string.Format("/pm {0} [{1}] {2}", playerName, chatName, chat);
        }

        public virtual string ApplyKickSyntax(string playerName, string reason, string chatName)
        {
            return string.Format("/kick {0} [{1}] {2}", playerName, chatName, reason);
        }
    }
}