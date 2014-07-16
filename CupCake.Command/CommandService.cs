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
            CommandPrefix = DefaultPrefix;
            ResponseMinGroup = Group.Moderator;

            ServiceLoader.EnableComplete += ServiceLoader_EnableComplete;
            Events.Bind<SayPlayerEvent>(OnSay, EventPriority.High);
        }

        private void ServiceLoader_EnableComplete(object sender, EventArgs e)
        {
            _chatService = ServiceLoader.Get<ChatService>();
        }

        private void OnSay(object sender, SayPlayerEvent e)
        {
            SynchronizePlatform.Do(() =>
            {
                if (e.Player.Say.StartsWith(CommandPrefix))
                {
                    InvokeFromPlayer(e.Player, new ParsedCommand(e.Player.Say.Substring(CommandPrefix.Length)));
                }
            });
        }

        public void InvokeFromPlayer(Player player, ParsedCommand message)
        {
            InvokeFromPlayer(player, player.GetGroup(), message);
        }

        public void InvokeFromPlayer(Player player, Group group, ParsedCommand message)
        {
            var source = new PlayerInvokeSource(this, group, player,
                (name, msg) =>
                    _chatService.Reply(player.Username, name, msg));
            Invoke(source, message);
        }

        public void InvokeFromConsole(ParsedCommand message)
        {
            InvokeFromConsole(Group.Host, message);
        }

        public void InvokeFromConsole(Group group, ParsedCommand message)
        {
            var source = new ConsoleInvokeSource(this, group,
                (name, msg) =>
                    Logger.LogPlatform.Log(name, LogPriority.Message, msg));

            Invoke(source, message);
        }

        public void Invoke(IInvokeSource source, ParsedCommand message)
        {
            var e = new InvokeEvent(source, message);
            Events.Raise(e);

            if (!e.Handled && source.Group >= ResponseMinGroup)
                source.Reply(UnknownCommandStr);
        }
    }
}