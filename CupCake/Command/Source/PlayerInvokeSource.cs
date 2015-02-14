using System.Diagnostics;
using BotBits;
using CupCake.Permissions;

namespace CupCake.Command.Source
{
    [DebuggerDisplay("Player = {Player}")]
    public class PlayerInvokeSource : InvokeSourceBase
    {
        public PlayerInvokeSource(object sender, Group @group, Player player, ReplyCallback onReply)
            : base(sender, @group, player.Username, onReply)
        {
            this.Player = player;
        }

        public Player Player { get; private set; }
    }
}