using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CupCake.Core
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class PluginNameAttribute : Attribute
    {
        public string Name { get; private set; }

        public PluginNameAttribute(string name)
        {
            this.Name = name;
        }
    }
}
