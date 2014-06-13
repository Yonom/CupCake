using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CupCake.Command
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class HighPriorityAttribute : Attribute
    {
    }
}
