using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CupCake.Command.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CorrectUsageAttribute : Attribute
    {
        public CorrectUsageAttribute(string usage)
        {
            this.Usage = usage;
        }

        public string Usage { get; private set; }
    }
}
