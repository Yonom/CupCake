using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CupCake.Command.Source;
using CupCake.Core.Events;
using CupCake.Permissions;

namespace CupCake.Command.Events
{
    public class ExternalInvokeEvent : Event
    {
        public ExternalInvokeSource Source { get; private set; }
        public string Message { get; set; }
        public Group Group { get; set; }

        public ExternalInvokeEvent(ExternalInvokeSource source, string message, Group group = Group.Host)
        {
            this.Source = source;
            this.Message = message;
            this.Group = @group;
        }
    }
}
