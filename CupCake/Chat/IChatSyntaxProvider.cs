namespace CupCake
{
    public interface IChatSyntaxProvider
    {
        /// <summary>
        ///     Runs whenever a text must be sent
        /// </summary>
        /// <param name="chat">The text being chatted</param>
        /// <param name="chatName">The chatName of the chatting plugin</param>
        string ApplyChatSyntax(string chat, string chatName);

        /// <summary>
        ///     Runs whenever Player.Reply, Command.Reply or Chatter.Reply is invoked
        /// </summary>
        /// <param name="chat">The text being chatted</param>
        /// <param name="playerName">The target player</param>
        /// <param name="chatName">The chatName of the chatting plugin</param>
        string ApplyPrivateMessageSyntax(string playerName, string chat, string chatName);

        /// <summary>
        ///     Runs whenever Player.Reply, Command.Reply or Chatter.Reply is invoked
        /// </summary>
        /// <param name="chat">The text being chatted</param>
        /// <param name="playerName">The target player</param>
        /// <param name="chatName">The chatName of the chatting plugin</param>
        string ApplyReplySyntax(string chat, string playerName, string chatName);

        /// <summary>
        ///     Runs whenever Chatter.Kick or Player.Kick is invoked
        /// </summary>
        /// <param name="playerName">The target player</param>
        /// <param name="reason">The kick reason</param>
        /// <param name="chatName">The chatName of the chatting plugin</param>
        string ApplyKickSyntax(string playerName, string reason, string chatName);
    }
}