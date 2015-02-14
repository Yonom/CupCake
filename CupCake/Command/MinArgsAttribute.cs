﻿using System;

namespace CupCake.Command
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class MinArgsAttribute : Attribute
    {
        public MinArgsAttribute(int minArgs)
        {
            this.MinArgs = minArgs;
        }

        public int MinArgs { get; private set; }
    }
}