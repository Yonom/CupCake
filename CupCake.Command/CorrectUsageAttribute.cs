using System;

namespace CupCake.Command
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class CorrectUsageAttribute : Attribute
    {
        public CorrectUsageAttribute(string usage)
        {
            Usage = usage;
        }

        public string Usage { get; private set; }
    }
}