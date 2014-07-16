using System;
using CupCake.Permissions;

namespace CupCake.Command
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class MinGroupAttribute : Attribute
    {
        public MinGroupAttribute(Group @group)
        {
            Group = @group;
        }

        public Group Group { get; private set; }
    }
}