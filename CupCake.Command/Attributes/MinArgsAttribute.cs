using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CupCake.Command.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MinArgsAttribute : Attribute
    {
        public int MinArgs { get; private set; }

        public MinArgsAttribute(int minArgs)
        {
            this.MinArgs = minArgs;
        }
    }
}
