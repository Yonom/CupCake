using System;
using BotBits;
using BotBits.Events;
using CupCake.Command.Source;
using CupCake.Log;
using CupCake.Permissions;

namespace CupCake.Command
{
    public sealed class CommandService : Service
    {
        private const string UnknownCommandStr = "Unknown command.";
        public const string DefaultPrefix = "!";
        public string CommandPrefix { get; set; }
        public Group ResponseMinGroup { get; set; } // TODO: do not allow changing this

        public CommandService()
        {
            this.CommandPrefix = DefaultPrefix;
            this.ResponseMinGroup = Group.Moderator;
        }

        [EventListener(EventPriority.High)]
        private void OnSay(ChatEvent e)
        {
            if (e.Text.StartsWith(this.CommandPrefix))
            {
                string command = e.Text.Substring(this.CommandPrefix.Length);
                if (String.IsNullOrEmpty(command))
                    return;

                this.InvokeFromPlayer(e.Player, new ParsedCommand(command));
            }
        }

        public void InvokeFromPlayer(Player player, ParsedCommand message)
        {
            this.InvokeFromPlayer(player, player.GetGroup(), message);
        }

        public void InvokeFromPlayer(Player player, Group group, ParsedCommand message)
        {
            var source = new PlayerInvokeSource(this, group, player,
                (name, msg) =>
                    this.Chatter.ChatService.Reply(player.Username, name, msg));
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
                    this.Logger.LogService.Log(name, LogPriority.Message, msg));

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