﻿using CupCake.Core.Events;
using CupCake.Messages.User;

namespace CupCake.Room
{
    public class AccessRightChangeEvent : Event
    {
        public AccessRightChangeEvent(AccessRight newRights)
        {
            this.NewRights = newRights;
        }

        public AccessRight NewRights { get; set; }
    }
}