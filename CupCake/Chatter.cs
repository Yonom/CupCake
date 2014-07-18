using System;
using CupCake.Chat;
using CupCake.Messages.User;
using JetBrains.Annotations;

namespace CupCake
{
    /// <summary>
    /// Class Chatter.
    /// Wraps a ChatService class and outputs customized messages with the given prefix.
    /// </summary>
    public class Chatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Chatter"/> class.
        /// </summary>
        /// <param name="chatService">The chat service being wrapped.</param>
        /// <param name="name">The prefix used for sending messages.</param>
        public Chatter(ChatService chatService, string name)
        {
            this.ChatService = chatService;
            this.Name = name;
        }

        /// <summary>
        /// Gets the chat service.
        /// </summary>
        /// <value>The chat service.</value>
        public ChatService ChatService { get; private set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Sends the specified chat message with the current chat style.
        /// </summary>
        /// <param name="msg">The chat message.</param>
        public void Chat(string msg)
        {
            this.ChatService.Chat(msg, this.Name);
        }

        /// <summary>
        /// Sends the specified chat message with the current chat style.
        /// </summary>
        /// <param name="msg">The chat message.</param>
        /// <param name="args">The object array that contains zero or more items to format.</param>
        [StringFormatMethod("msg")]
        public void Chat(string msg, params object[] args)
        {
            this.ChatService.Chat(String.Format(msg, args), this.Name);
        }

        /// <summary>
        /// Sends the specified chat message without formatting.
        /// </summary>
        /// <param name="msg">The chat message.</param>
        public void Send(string msg)
        {
            this.ChatService.Send(msg);
        }

        /// <summary>
        /// Sends the specified chat message without formatting.
        /// </summary>
        /// <param name="msg">The chat message.</param>
        /// <param name="args">The object array that contains zero or more items to format.</param>
        [StringFormatMethod("msg")]
        public void Send(string msg, params object[] args)
        {
            this.ChatService.Send(String.Format(msg, args));
        }

        /// <summary>
        /// Sends a message with reply formatting, targeting the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="msg">The chat message.</param>
        public void Reply(string username, string msg)
        {
            this.ChatService.Reply(username, this.Name, msg);
        }

        /// <summary>
        /// Sends a message with reply formatting, targeting the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="msg">The chat message.</param>
        /// <param name="args">The object array that contains zero or more items to format.</param>
        [StringFormatMethod("msg")]
        public void Reply(string username, string msg, params object[] args)
        {
            this.ChatService.Reply(username, this.Name, String.Format(msg, args));
        }

        /// <summary>
        /// Gives edit to the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        public void GiveEdit(string username)
        {
            this.ChatService.GiveEdit(username);
        }

        /// <summary>
        /// Removes edit from the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        public void RemoveEdit(string username)
        {
            this.ChatService.RemoveEdit(username);
        }

        /// <summary>
        /// Teleports the specified username to the bot's location. (/teleport &lt;username&gt;).
        /// </summary>
        /// <param name="username">The username.</param>
        public void Teleport(string username)
        {
            this.ChatService.Teleport(username);
        }

        /// <summary>
        /// Teleports the specified username. (/teleport &lt;username&gt; &lt;x&gt; &lt;y&gt;).
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public void Teleport(string username, int x, int y)
        {
            this.ChatService.Teleport(username, x, y);
        }

        /// <summary>
        /// Kicks the specified username. (/kick &lt;username&gt;).
        /// </summary>
        /// <param name="username">The username.</param>
        public void Kick(string username)
        {
            this.ChatService.Kick(this.Name, username);
        }

        /// <summary>
        /// Kicks the specified username. (/kick &lt;username&gt; &lt;reason&gt;).
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="reason">The kick reason.</param>
        public void Kick(string username, string reason)
        {
            this.ChatService.Kick(this.Name, username, reason);
        }

        /// <summary>
        /// Kicks the specified username. (/kick &lt;username&gt; &lt;reason&gt;).
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="reason">The kick reason.</param>
        /// <param name="args">The object array that contains zero or more items to format.</param>
        [StringFormatMethod("reason")]
        public void Kick(string username, string reason, params object[] args)
        {
            this.ChatService.Kick(this.Name, username, String.Format(reason, args));
        }

        /// <summary>
        /// Silently kicks all guests (/kickguests).
        /// </summary>
        public void KickGuests()
        {
            this.ChatService.KickGuests();
        }

        /// <summary>
        /// Kills the specified username. (/kill &lt;username&gt;).
        /// </summary>
        /// <param name="username">The username.</param>
        public void Kill(string username)
        {
            this.ChatService.Kill(username);
        }

        /// <summary>
        /// Mutes the specified username. (/mute &lt;username&gt;).
        /// </summary>
        /// <param name="username">The username.</param>
        public void Mute(string username)
        {
            this.ChatService.Mute(username);
        }

        /// <summary>
        /// Unmutes the specified username. (/unmute &lt;username&gt;).
        /// </summary>
        /// <param name="username">The username.</param>
        public void Unmute(string username)
        {
            this.ChatService.Unmute(username);
        }

        /// <summary>
        /// Reports the specified user with the given reason (/reportabuse &lt;username&gt; &lt;reason&gt;).
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="reason">The reason.</param>
        public void ReportAbuse(string username, string reason)
        {
            this.ChatService.ReportAbuse(username, reason);
        }

        /// <summary>
        /// Kills all the users in the world (/killemall).
        /// </summary>
        public void KillAll()
        {
            this.ChatService.KillAll();
        }

        /// <summary>
        /// Resets all the users' positions (/reset).
        /// </summary>
        public void Reset()
        {
            this.ChatService.Reset();
        }

        /// <summary>
        /// Respawns the bot (/respawn).
        /// </summary>
        public void Respawn()
        {
            this.ChatService.Respawn();
        }

        /// <summary>
        /// Respawns all users in the world (/respawnall).
        /// </summary>
        public void RespawnAll()
        {
            this.ChatService.RespawnAll();
        }

        /// <summary>
        /// Enables the given potions (/potionson &lt;potion1&gt; &lt;potion2&gt; ...).
        /// </summary>
        /// <param name="potions">The potions.</param>
        public void PotionsOn(params string[] potions)
        {
            this.ChatService.PotionsOn(potions);
        }

        /// <summary>
        /// Enables the given potions (/potionson &lt;potion1&gt; &lt;potion2&gt; ...).
        /// </summary>
        /// <param name="potions">The potions.</param>
        public void PotionsOn(params int[] potions)
        {
            this.ChatService.PotionsOn(potions);
        }

        /// <summary>
        /// Enables the given potions (/potionson &lt;potion1&gt; &lt;potion2&gt; ...).
        /// </summary>
        /// <param name="potions">The potions.</param>
        public void PotionsOn(params Potion[] potions)
        {
            this.ChatService.PotionsOn(potions);
        }

        /// <summary>
        /// Disables the given potions (/potionsoff &lt;potion1&gt; &lt;potion2&gt; ...).
        /// </summary>
        /// <param name="potions">The potions.</param>
        public void PotionsOff(params string[] potions)
        {
            this.ChatService.PotionsOff(potions);
        }

        /// <summary>
        /// Disables the given potions (/potionsoff &lt;potion1&gt; &lt;potion2&gt; ...).
        /// </summary>
        /// <param name="potions">The potions.</param>
        public void PotionsOff(params int[] potions)
        {
            this.ChatService.PotionsOff(potions);
        }

        /// <summary>
        /// Disables the given potions (/potionsoff &lt;potion1&gt; &lt;potion2&gt; ...).
        /// </summary>
        /// <param name="potions">The potions.</param>
        public void PotionsOff(params Potion[] potions)
        {
            this.ChatService.PotionsOff(potions);
        }

        /// <summary>
        /// Changes the visibility of the room (/visible &lt;visible&gt;).
        /// </summary>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        public void ChangeVisibility(bool visible)
        {
            this.ChatService.ChangeVisibility(visible);
        }

        /// <summary>
        /// Loads the level to the most recent saved version (/loadlevel).
        /// </summary>
        public void LoadLevel()
        {
            this.ChatService.LoadLevel();
        }
    }
}