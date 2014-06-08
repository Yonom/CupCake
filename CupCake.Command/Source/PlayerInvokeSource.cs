using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.Command.Source
{
    public class PlayerInvokeSource : InvokeSourceBase
    {
        public PlayerInvokeSource(object sender, Group @group, Player player, ReplyCallback onReply)
            : base(sender, group, onReply)
        {
            this.Player = player;
        }

        public Player Player { get; private set; }
    }
}