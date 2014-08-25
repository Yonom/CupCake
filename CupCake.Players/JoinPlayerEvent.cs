using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class JoinPlayerEvent : PlayerEvent<IUserPosReceiveEvent>
    {
        internal JoinPlayerEvent(Player oldPlayer, Player player, IUserPosReceiveEvent innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }
    }
}