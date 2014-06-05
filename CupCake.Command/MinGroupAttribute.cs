using System;
using CupCake.Permissions;

namespace CupCake.Command
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MinGroupAttribute : Attribute
    {
        public MinGroupAttribute(Group @group)
        {
            this.Group = @group;
        }

        public Group Group { get; private set; }
    }
}