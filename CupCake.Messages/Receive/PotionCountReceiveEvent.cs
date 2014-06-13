using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class PotionCountReceiveEvent : ReceiveEvent
    {
        //No arguments

        public PotionCountReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}
