using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class JoinPlayerEvent : PlayerEvent<IUserPosReceiveEvent>
    {
        internal JoinPlayerEvent(Player player, IUserPosReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}