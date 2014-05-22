using System;
using System.Collections.Generic;
using System.Linq;
using MuffinFramework.Platforms;

namespace CupCake.Core.Events
{
    public class EventHandle<T> : PlatformPart<object> where T : Event
    {
        private readonly LinkedList<EventHandler<T>> _eventHandlers = new LinkedList<EventHandler<T>>();

        public int Count
        {
            get
            {
                lock (this._eventHandlers)
                {
                    return this._eventHandlers.Count;
                }
            }
        }

        protected override void Enable()
        {
        }

        public void Clear()
        {
            lock (this._eventHandlers)
            {
                this._eventHandlers.Clear();
            }
        }

        public bool Contains(EventHandler<T> item)
        {
            lock (this._eventHandlers)
            {
                return this._eventHandlers.Contains(item);
            }
        }

        public bool Remove(EventHandler<T> item)
        {
            lock (this._eventHandlers)
            {
                return this._eventHandlers.Remove(item);
            }
        }

        public void Bind(EventHandler<T> callback, EventPriority priority = EventPriority.Normal)
        {
            lock (this._eventHandlers)
            {
                switch (priority)
                {
                    case EventPriority.Normal:
                        this._eventHandlers.AddLast(callback);
                        break;
                    case EventPriority.BeforeMost:
                        this._eventHandlers.AddFirst(callback);
                        break;
                    default:
                        throw new InvalidOperationException("Unknown priority.");
                }
            }
        }

        public void Raise(object sender, T e)
        {
            EventHandler<T>[] handlers;
            lock (this._eventHandlers)
            {
                handlers = this._eventHandlers.ToArray();
            }

            foreach (var handler in handlers)
            {
                handler.Invoke(sender, e);
            }
        }
    }
}