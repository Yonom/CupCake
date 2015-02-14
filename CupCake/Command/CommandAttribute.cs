﻿using System;
using JetBrains.Annotations;

namespace CupCake.Command
{
    /// <summary>
    ///     Indicates that a method handles a command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method), MeansImplicitUse]
    public sealed class CommandAttribute : LabelAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandAttribute" /> class.
        /// </summary>
        /// <param name="labels">The labels for this command. Exclude the "!" character</param>
        public CommandAttribute(params string[] labels)
            : base(labels)
        {
        }
    }
}