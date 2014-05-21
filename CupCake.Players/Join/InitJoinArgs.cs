using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.EE.Events.Receive;

namespace CupCake.Players.Join
{
    internal class InitJoinArgs : JoinArgs
    {
        public InitReceiveEvent InitReceiveEvent { get; private set; }

        public InitJoinArgs(InitReceiveEvent initReceiveEvent)
        {
            this.InitReceiveEvent = initReceiveEvent;
        }
    }
}
