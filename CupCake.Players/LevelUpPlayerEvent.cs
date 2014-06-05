using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class LevelUpPlayerEvent : PlayerEvent<LevelUpReceiveEvent>
    {
        public LevelUpPlayerEvent(Player player, LevelUpReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}