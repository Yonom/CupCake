using System;
using BotBits;
using JetBrains.Annotations;

namespace CupCake
{
    /// <summary>
    ///     Class Chatter.
    ///     Wraps a ChatService class and outputs customized messages with the given prefix.
    /// </summary>
    public class Chatter : IChat
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Chatter" /> class.
        /// </summary>
        /// <param name="chatService">The chat service being wrapped.</param>
        /// <param name="name">The prefix used for sending messages.</param>
        public Chatter(ChatService chatService, string name)
        {
            this.ChatService = chatService;
            this.Name = name;
        }

        /// <summary>
        ///     Gets the chat service.
        /// </summary>
        /// <value>The chat service.</value>
        public ChatService ChatService { get; private set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; } // TODO make this settable from a property in Plugin

        /// <summary>
        ///     Sends the specified chat message with the current chat style.
        /// </summary>
        /// <param name="msg">The chat message.</param>
        public void Say(string msg)
        {
            this.ChatService.Say(msg, this.Name);
        }

        /// <summary>
        ///     Sends the specified chat message without formatting.
        /// </summary>
        /// <param name="msg">The chat message.</param>
        public void Send(string msg)
        {
            this.ChatService.Send(msg);
        }

        /// <summary>
        ///     Sends the specified chat message without formatting.
        /// </summary>
        /// <param name="msg">The chat message.</param>
        /// <param name="args">The object array that contains zero or more items to format.</param>
        [StringFormatMethod("msg")]
        public void Send(string msg, params object[] args)
        {
            this.ChatService.Send(String.Format(msg, args));
        }

        /// <summary>
        ///     Sends a message with reply formatting, targeting the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="msg">The chat message.</param>
        public void Reply(string username, string msg)
        {
            this.ChatService.Reply(username, this.Name, msg);
        }

        /// <summary>
        ///     Sends a message with reply formatting, targeting the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="msg">The chat message.</param>
        /// <param name="args">The object array that contains zero or more items to format.</param>
        [StringFormatMethod("msg")]
        public void Reply(string username, string msg, params object[] args)
        {
            this.ChatService.Reply(username, this.Name, String.Format(msg, args));
        }
        // TODO: allow replying to players
    }
}