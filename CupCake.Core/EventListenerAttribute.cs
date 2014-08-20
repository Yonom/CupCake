using System;
using CupCake.Core.Events;

namespace CupCake.Core
{
    /// <summary>
    /// Indicates that a function is a handler for a specific event.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class EventListenerAttribute : Attribute
    {
        /// <summary>
        /// Gets the priority of this event handler.
        /// </summary>
        /// <value>
        /// The priority of this event handler.
        /// </value>
        public EventPriority Priority { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventListenerAttribute"/> class.
        /// </summary>
        public EventListenerAttribute()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventListenerAttribute"/> class.
        /// </summary>
        /// <param name="priority">The priority of this event handler.</param>
        public EventListenerAttribute(EventPriority priority)
        {
            this.Priority = priority;
        }
    }
}
