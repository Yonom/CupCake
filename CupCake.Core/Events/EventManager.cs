using System;
using System.Collections.Generic;
using CupCake.Core.Platforms;

namespace CupCake.Core.Events
{
    public class EventManager : IDisposable
    {
        private readonly List<IBinding> _bindings = new List<IBinding>();
        private readonly object _sender;

        public EventManager(EventsPlatform eventsPlatform, object sender)
        {
            this._sender = sender;
            this.EventsPlatform = eventsPlatform;
        }

        public EventsPlatform EventsPlatform { get; set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Bind<T>(EventHandler<T> callback, EventPriority priority = EventPriority.Normal) where T : Event
        {
            lock (this._bindings)
            {
                var binding = new Binding<T>(this, callback, priority);
                binding.Subscribe();
                this._bindings.Add(binding);
            }
        }

        public void Raise<T>(T eventArgs) where T : Event
        {
            lock (this._bindings)
            {
                this.EventsPlatform.Event<T>().Raise(this._sender, eventArgs);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (this._bindings)
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

            public void Subscribe()
            {
                if (!this._parent.EventsPlatform.Event<T>().Contains(this._callback))
                {
                    this._parent.EventsPlatform.Event<T>().Bind(this._callback, this._priority);
                }
            }

            public void Unsubscribe()
            {
                if (this._parent.EventsPlatform.Event<T>().Contains(this._callback))
                {
                    this._parent.EventsPlatform.Event<T>().Remove(this._callback);
                }
            }
        }

        private interface IBinding
        {
            void Subscribe();
            void Unsubscribe();
        }
    }
}