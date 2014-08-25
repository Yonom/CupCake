using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class LevelUpPlayerEvent : PlayerEvent<LevelUpReceiveEvent>
    {
        internal LevelUpPlayerEvent(Player oldPlayer, Player player, LevelUpReceiveEvent innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }
    }
}