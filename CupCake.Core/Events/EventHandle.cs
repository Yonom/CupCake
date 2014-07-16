using System;
using System.Collections.Generic;
using System.Linq;
using MuffinFramework.Platforms;

namespace CupCake.Core.Events
{
    public class EventHandle<T> : PlatformPart<object> where T : Event
    {
        private readonly Dictionary<EventPriority, IList<EventHandler<T>>> _eventHandlers =
            new Dictionary<EventPriority, IList<EventHandler<T>>>();

        public int Count
        {
            get
            {
                lock (_eventHandlers)
                {
                    return _eventHandlers.Count;
                }
            }
        }

        protected override void Enable()
        {
        }

        public void Clear()
        {
            lock (_eventHandlers)
            {
                _eventHandlers.Clear();
            }
        }

        public bool Contains(EventHandler<T> item)
        {
            lock (_eventHandlers)
            {
                return _eventHandlers.Values.Any(handlerGroup => handlerGroup.Contains(item));
            }
        }

        public bool Remove(EventHandler<T> item)
        {
            lock (_eventHandlers)
            {
                return _eventHandlers.Values.Any(handlerGroup => handlerGroup.Remove(item));
            }
        }

        public void Bind(EventHandler<T> callback, EventPriority priority = EventPriority.Normal)
        {
            lock (_eventHandlers)
            {
                if (!_eventHandlers.ContainsKey(priority))
                    _eventHandlers.Add(priority, new List<EventHandler<T>>());

                _eventHandlers[priority].Add(callback);
            }
        }

        public void Raise(object sender, T e)
        {
            EventHandler<T>[] handlers;
            lock (_eventHandlers)
            {
                // Create an array of all handlers in the right order
                handlers = _eventHandlers.OrderBy(k => k.Key)
                    .SelectMany(k => k.Value).ToArray();
            }

            foreach (var handler in handlers)
            {
                handler(sender, e);
            }
        }
    }
}