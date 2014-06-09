using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class JoinPlayerEvent : PlayerEvent<IUserPosReceiveEvent>
    {
        public JoinPlayerEvent(Player player, IUserPosReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}
