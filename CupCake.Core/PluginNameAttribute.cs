using System;

namespace CupCake.Core
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class PluginNameAttribute : Attribute
    {
        public PluginNameAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}