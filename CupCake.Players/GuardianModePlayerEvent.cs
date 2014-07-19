using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class GuardianModePlayerEvent : PlayerEvent<GuardianModeReceiveEvent>
    {
        internal GuardianModePlayerEvent(Player player, GuardianModeReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}
