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
        public string CommandPrefix { get; set; }

        protected override void Enable()
        {
            this.Events.Bind<SayPlayerEvent>(this.OnSay);
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
            this.Events.Raise(new PlayerInvokeEvent(player, message));
        }

        public void InvokeFromPlayer(Player player, ParsedCommand message, Group group)
        {
            this.Events.Raise(new PlayerInvokeEvent(player, message, group));
        }

        public void Invoke(IInvokeSource source, ParsedCommand message, Group group)
        {
            this.Events.Raise(new InvokeEvent(source, message, group));
        }
    }
}