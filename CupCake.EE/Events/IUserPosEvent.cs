using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CupCake.EE.Events
{
    interface IUserPosEvent : IUserEvent
    {
        int UserPosX { get; }
        int UserPosY { get; }
    }
}
