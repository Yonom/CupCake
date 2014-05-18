using System;
using System.Collections;
using System.Collections.Generic;

namespace CupCake.Core.Events
{
    public class EventManager<T> : ICollection<EventHandler<T>> where T : EventArgs
    {
        private static readonly Dictionary<int, EventManager<T>> _eventManagers = new Dictionary<int, EventManager<T>>();

        private readonly LinkedList<EventHandler<T>> _eventHandlers = new LinkedList<EventHandler<T>>();

        private EventManager()
        {
        }

        public IEnumerator<EventHandler<T>> GetEnumerator()
        {
            return this._eventHandlers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(EventHandler<T> item)
        {
            this.Bind(item);
        }

        public void Clear()
        {
            this._eventHandlers.Clear();
        }

        public bool Contains(EventHandler<T> item)
        {
            return this._eventHandlers.Contains(item);
        }

        public void CopyTo(EventHandler<T>[] array, int arrayIndex)
        {
            this._eventHandlers.CopyTo(array, arrayIndex);
        }

        public bool Remove(EventHandler<T> item)
        {
            this._eventHandlers.Remove(item);
        }

        public int Count
        {
            get { return this._eventHandlers.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        internal static EventManager<T> Get(int id)
        {
            if (!_eventManagers.ContainsKey(id))
                _eventManagers[id] = new EventManager<T>();

            return _eventManagers[id];
        }

        public void Bind(EventHandler<T> callback)
        {
            this.Bind(callback, EventPriority.Normal);
        }

        public void Bind(EventHandler<T> callback, EventPriority priority)
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
}