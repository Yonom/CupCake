using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Core.Events;

namespace CupCake.HostAPI.Status
{
    public class ChangeStatusEvent : Event
    {
        public string NewStatus { get; set; }

        public ChangeStatusEvent(string newStatus)
        {
            this.NewStatus = newStatus;
        }
    }
}
