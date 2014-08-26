using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Core.Events;

namespace CupCake.HostAPI
{
    public class ShutdownRequestEvent : Event
    {
        public bool IsRestarting { get; private set; }

        internal ShutdownRequestEvent(bool isRestarting)
        {
            this.IsRestarting = isRestarting;
        }
    }
}
