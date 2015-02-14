using System;

namespace CupCake.Command
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class HighPriorityAttribute : Attribute
    {
    }
}