using CupCake.Messages.Events.Receive;

namespace CupCake.Players.Events
{
    public class LevelUpPlayerEvent : PlayerEvent<LevelUpReceiveEvent>
    {
        public LevelUpPlayerEvent(Player player, LevelUpReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}