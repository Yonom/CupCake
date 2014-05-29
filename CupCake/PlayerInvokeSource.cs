using CupCake.Command.Source;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake
{
    public class PlayerInvokeSource : IInvokeSource
    {
        private readonly Chatter _chatter;

        public PlayerInvokeSource(object sender, Group @group, Player player, Chatter chatter)
        {
            this._chatter = chatter;
            this.Group = @group;
            this.Sender = sender;
            this.Player = player;
        }

        public Player Player { get; private set; }
        public object Sender { get; private set; }
        public Group Group { get; private set; }

        public void Reply(string message)
        {
            this._chatter.Reply(this.Player.Username, message);
        }
    }
}