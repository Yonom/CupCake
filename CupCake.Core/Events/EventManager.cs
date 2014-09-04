using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CupCake.Core.Events
{
    public class EventManager : IEnumerable<EventManager.IBinding>, IDisposable
    {
        private readonly List<IBinding> _bindings = new List<IBinding>();
        private readonly object _lockObj = new object();
        private readonly object _sender;

        public EventManager(EventsPlatform eventsPlatform, object sender)
        {
            this._sender = sender;
            this.EventsPlatform = eventsPlatform;
        }

        public EventsPlatform EventsPlatform { get; private set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Bind<T>(EventHandler<T> callback, EventPriority priority = EventPriority.Normal) where T : Event
        {
            if (this.Contains(callback))
            {
                throw new ArgumentException("Callback has already been added to the specified event.");
            }

            var binding = new Binding<T>(this, callback, priority);
            binding.Subscribe();

            lock (this._lockObj)
            {
                this._bindings.Add(binding);
            }
        }

        public bool Contains<T>(EventHandler<T> callback) where T : Event
        {
            lock (this._lockObj)
            {
                return this._bindings.Any(
                    binding =>
                        typeof(T) == binding.Type &&
                        binding.GetCallback() == (Delegate)callback);
            }
        }

        public bool TryGetBinding<T>(EventHandler<T> callback, out IBinding binding) where T : Event
        {
            lock (this._lockObj)
            {
                foreach (IBinding b in 
                    this._bindings.Where(b =>
                        typeof(T) == b.Type &&
                        b.GetCallback() == (Delegate)callback))
                {
                    binding = b;
                    return true;
                }

                binding = null;
                return false;
            }
        }

        public void Raise<T>(T eventArgs) where T : Event
        {
            this.EventsPlatform.Event<T>().Raise(this._sender, eventArgs);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (this._lockObj)
                {
                    foreach (IBinding binding in this._bindings)
                    {
                        binding.Unsubscribe();
                    }
                }
            }
        }

        private class Binding<T> : IBinding where T : Event
        {
            private readonly EventHandler<T> _callback;
            private readonly EventManager _parent;
            private readonly EventPriority _priority;

            public Binding(EventManager parent, EventHandler<T> callback, EventPriority priority)
            {
                this._parent = parent;
                this._callback = callback;
                this._priority = priority;
            }

            public Type Type
            {
                get { return typeof(T); }
            }

            public bool IsSubscribed
            {
                get { return this._parent.EventsPlatform.Event<T>().Contains(this._callback); }
            }

            public Delegate GetCallback()
            {
                return this._callback;
            }

            public void Subscribe()
            {
                this._parent.EventsPlatform.Event<T>().Bind(this._callback, this._priority);
            }

            public void Unsubscribe()
            {
                this._parent.EventsPlatform.Event<T>().Remove(this._callback);
            }
        }

        public interface IBinding
        {
            Type Type { get; }
            bool IsSubscribed { get; }
            Delegate GetCallback();
            void Subscribe();
            void Unsubscribe();
        }

        public IEnumerator<IBinding> GetEnumerator()
        {
            lock (this._lockObj)
            {
                return this._bindings.ToArray().AsEnumerable().GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}