using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CupCake.Command.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class LabelAttribute : Attribute
    {
        public string[] Labels { get; set; }

        public LabelAttribute(params string[] labels)
        {
            this.Labels = labels;
        }
    }
}
