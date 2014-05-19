using System;
using System.Collections.Generic;

namespace CupCake.Core.Events
{
    public class EventManager<T> where T : EventArgs
    {
        private static readonly Dictionary<int, EventManager<T>> _eventManagers = new Dictionary<int, EventManager<T>>();

        private readonly LinkedList<EventHandler<T>> _eventHandlers = new LinkedList<EventHandler<T>>();

        private EventManager()
        {
        }

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

        internal static EventManager<T> Get(int id)
        {
            lock (_eventManagers)
            {
                if (!_eventManagers.ContainsKey(id))
                    _eventManagers[id] = new EventManager<T>();

                return _eventManagers[id];
            }
        }

        public void Bind(EventHandler<T> callback)
        {
            this.Bind(callback, EventPriority.Normal);
        }

        public void Bind(EventHandler<T> callback, EventPriority priority)
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
            lock (this._eventHandlers)
            {
                foreach (var handler in this._eventHandlers)
                {
                    handler.Invoke(sender, e);
                }
            }
        }
    }
}