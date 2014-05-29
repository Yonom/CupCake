using System.Globalization;
using CupCake.Chat;

namespace CupCake.Server.SyntaxProviders
{
    public class CupCakeChatSyntaxProvider : IChatSyntaxProvider
    {
        public virtual string ApplyChatSyntax(string chat, string chatName)
        {
            return string.Format("<{0}> {1}", chatName, chat);
        }

        public virtual string ApplyReplySyntax(string chat, string chatName, string playerName)
        {
            return string.Format("<{0} (@{1})> {2}", chatName, MakeFirstLetterUpperCase(playerName), chat);
        }

        public virtual string ApplyKickSyntax(string chatName, string playerName, string reason)
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