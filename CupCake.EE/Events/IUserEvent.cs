using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CupCake.EE.Events
{
    public interface IUserEvent
    {
        int UserId { get; }
    }
}
