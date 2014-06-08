using System;

namespace CupCake.Command
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CorrectUsageAttribute : Attribute
    {
        public CorrectUsageAttribute(string usage)
        {
            this.Usage = usage;
        }

        public string Usage { get; private set; }
    }
}