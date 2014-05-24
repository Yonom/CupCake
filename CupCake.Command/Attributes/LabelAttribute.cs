using System;

namespace CupCake.Command.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class LabelAttribute : Attribute
    {
        public LabelAttribute(params string[] labels)
        {
            this.Labels = labels;
        }

        public string[] Labels { get; set; }
    }
}