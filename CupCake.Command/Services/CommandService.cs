using CupCake.Chat.Services;
using CupCake.Command.Events;
using CupCake.Command.Source;
using CupCake.Core.Services;
using CupCake.Permissions;
using CupCake.Players;
using CupCake.Players.Events;

namespace CupCake.Command.Services
{
    public class CommandService : CupCakeService
    {
        public const string DefaultPrefix = "!";
        public string CommandPrefix { get; set; }
        public Group ResponseMinGroup { get; set; }
        private ChatService _chatService;
        private const string UnknownCommandStr = "Unknown command.";

        protected override void Enable()
        {
            this.CommandPrefix = DefaultPrefix;
            this.ResponseMinGroup = Group.Moderator;

            this.ServiceLoader.EnableComplete += ServiceLoader_EnableComplete;
            this.Events.Bind<SayPlayerEvent>(this.OnSay);
        }

        private void ServiceLoader_EnableComplete(object sender, System.EventArgs e)
        {
            this._chatService = this.ServiceLoader.Get<ChatService>();
        }

        private void OnSay(object sender, SayPlayerEvent e)
        {
            if (e.Player.Say.StartsWith(this.CommandPrefix))
            {
                this.InvokeFromPlayer(e.Player, new ParsedCommand(e.Player.Say.Substring(this.CommandPrefix.Length)));
            }
        }

        public void InvokeFromPlayer(Player player, ParsedCommand message)
        {
            this.InvokeFromPlayer(player, message, player.GetGroup());
        }

        public void InvokeFromPlayer(Player player, ParsedCommand message, Group group)
        {
            var e = new PlayerInvokeEvent(player, message, group);
            this.Events.Raise(e);

            if (!e.Handled && group >= ResponseMinGroup)
                this._chatService.Reply(player.Username, "Bot", UnknownCommandStr);
        }

        public void Invoke(IInvokeSource source, ParsedCommand message)
        {
            var e = new InvokeEvent(source, message);
            this.Events.Raise(e);

            if (!e.Handled && source.Group >= ResponseMinGroup)
                source.Reply(UnknownCommandStr);
        }
    }
}