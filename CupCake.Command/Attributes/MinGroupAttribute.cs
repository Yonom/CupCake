using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Permissions;

namespace CupCake.Command.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MinGroupAttribute : Attribute
    {
        public Group Group { get; private set; }

        public MinGroupAttribute(Group @group)
        {
            this.Group = @group;
        }
    }
}
