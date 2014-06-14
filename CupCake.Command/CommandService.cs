using System;
using CupCake.Chat;
using CupCake.Command.Source;
using CupCake.Core;
using CupCake.Core.Events;
using CupCake.Core.Log;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.Command
{
    public sealed class CommandService : CupCakeService
    {
        public const string DefaultPrefix = "!";
        private const string UnknownCommandStr = "Unknown command.";
        private ChatService _chatService;
        public string CommandPrefix { get; set; }
        public Group ResponseMinGroup { get; set; }

        protected override void Enable()
        {
            this.CommandPrefix = DefaultPrefix;
            this.ResponseMinGroup = Group.Moderator;

            this.ServiceLoader.EnableComplete += this.ServiceLoader_EnableComplete;
            this.Events.Bind<SayPlayerEvent>(this.OnSay, EventPriority.High);
        }

        private void ServiceLoader_EnableComplete(object sender, EventArgs e)
        {
            this._chatService = this.ServiceLoader.Get<ChatService>();
        }

        private void OnSay(object sender, SayPlayerEvent e)
        {
            this.SynchronizePlatform.Do(() =>
            {
                if (e.Player.Say.StartsWith(this.CommandPrefix))
                {
                    this.InvokeFromPlayer(e.Player, new ParsedCommand(e.Player.Say.Substring(this.CommandPrefix.Length)));
                }
            });
        }

        public void InvokeFromPlayer(Player player, ParsedCommand message)
        {
            this.InvokeFromPlayer(player, player.GetGroup(), message);
        }

        public void InvokeFromPlayer(Player player, Group group, ParsedCommand message)
        {
            var source = new PlayerInvokeSource(this, group, player,
                (name, msg) =>
                    this._chatService.Reply(player.Username, msg, name));
            this.Invoke(source, message);
        }

        public void InvokeFromConsole(ParsedCommand message)
        {
            this.InvokeFromConsole(Group.Host, message);
        }

        public void InvokeFromConsole(Group group, ParsedCommand message)
        {
            var source = new ConsoleInvokeSource(this, group,
                (name, msg) =>
                    this.Logger.LogPlatform.Log(name, LogPriority.Message, msg));

            this.Invoke(source, message);
        }

        public void Invoke(IInvokeSource source, ParsedCommand message)
        {
            var e = new InvokeEvent(source, message);
            this.Events.Raise(e);

            if (!e.Handled && source.Group >= this.ResponseMinGroup)
                source.Reply(UnknownCommandStr);
        }
    }
}