using System;
using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class AddPlayerEvent : PlayerEvent<AddReceiveEvent>
    {
        internal AddPlayerEvent(Player oldPlayer, Player player, AddReceiveEvent innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }

        public override Player OldPlayer
        {
            get { throw new NotSupportedException("OldPlayer is not supported on AddPlayerEvent."); }
        }
    }
}