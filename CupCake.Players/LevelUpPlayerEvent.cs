using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class LevelUpPlayerEvent : PlayerEvent<LevelUpReceiveEvent>
    {
        internal LevelUpPlayerEvent(Player player, LevelUpReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}