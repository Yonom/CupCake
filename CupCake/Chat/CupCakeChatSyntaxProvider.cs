using System.Globalization;
using BotBits;

namespace CupCake
{
    public class CupCakeChatSyntaxProvider : IChatSyntaxProvider
    {
        public virtual string ApplyChatSyntax(string chat, string chatName)
        {
            return string.Format("<{0}> {1}", chatName, chat);
        }

        public virtual string ApplyReplySyntax(string chat, string playerName, string chatName)
        {
            return string.Format("<{0} (@{1})> {2}", chatName, MakeFirstLetterUpperCase(playerName), chat);
        }

        public string ApplyPrivateMessageSyntax(string playerName, string chat, string chatName)
        {
            return string.Format("/pm {0} <{1}> {2}", playerName, chatName, chat);
        }

        public virtual string ApplyKickSyntax(string playerName, string reason, string chatName)
        {
            return string.Format("/kick {0} <{1}> {2}", playerName, chatName, reason);
        }

        private static string MakeFirstLetterUpperCase(string value)
        {
            char[] letters = value.ToCharArray();
            letters[0] = char.ToUpper(letters[0], CultureInfo.InvariantCulture);

            return new string(letters);
        }
    }
}