using System;
using System.Linq;
using BotBits;

namespace CupCake
{
    public sealed class ChatService : Service
    {
        public IChatSyntaxProvider SyntaxProvider { get; set; }

        public ChatService()
        {
            this.SyntaxProvider = new CupCakeChatSyntaxProvider();
        }

        public void Send(string msg)
        {
            this.Chat.Say(msg);
        }
        
        public void Say(string msg, string chatName)
        {
            if (msg.StartsWith("/", StringComparison.Ordinal))
            {
                var cmdArgs = msg.Substring(1).Split(' ');
                if (cmdArgs[0] == "kick" && cmdArgs.Length > 1)
                {
                    var user = cmdArgs[1];
                    var reason = String.Empty;
                    if (cmdArgs.Length > 2)
                        reason = String.Join(" ", cmdArgs, 2, cmdArgs.Length - 2);

                    this.Send(this.SyntaxProvider.ApplyKickSyntax(user, reason, chatName));
                }
                else if (cmdArgs[0] == "pm" && cmdArgs.Length > 2)
                {
                    var user = cmdArgs[1];
                    var message = String.Join(" ", cmdArgs, 2, cmdArgs.Length - 2);
                    this.Send(this.SyntaxProvider.ApplyPrivateMessageSyntax(user, message, chatName));
                }
                else
                {
                    this.Send(msg);
                }
            }
            else
            {
                this.Send(this.SyntaxProvider.ApplyChatSyntax(msg, chatName));
            }
        }

        public void Reply(string username, string msg, string chatName)
        {
            this.Send(this.SyntaxProvider.ApplyReplySyntax(msg, username, chatName));
        }
    }
}