using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Core.Events;
using CupCake.EE;

namespace CupCake.Room.Events
{
    public class AccessRightChangeEvent : Event
    {
        public AccessRight NewRights { get; set; }

        public AccessRightChangeEvent(AccessRight newRights)
        {
            this.NewRights = newRights;
        }
    }
}
