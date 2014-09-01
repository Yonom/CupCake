using System;
using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class JoinPlayerEvent : PlayerEvent<IUserPosReceiveEvent>
    {
        internal JoinPlayerEvent(Player oldPlayer, Player player, IUserPosReceiveEvent innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }

        public override Player OldPlayer
        {
            get { throw new NotSupportedException("OldPlayer is not supported on JoinPlayerEvent."); }
        }
    }
}