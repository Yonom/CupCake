using System;

namespace CupCake.Command
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class LabelAttribute : Attribute
    {
        public LabelAttribute(params string[] labels)
        {
            this.Labels = labels;
        }

        public string[] Labels { get; private set; }
    }
}