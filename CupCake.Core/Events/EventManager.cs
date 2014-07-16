using System;
using System.Collections.Concurrent;
using System.Linq;

namespace CupCake.Core.Events
{
    public class EventManager : IDisposable
    {
        private readonly ConcurrentBag<IBinding> _bindings = new ConcurrentBag<IBinding>();
        private readonly object _sender;

        public EventManager(EventsPlatform eventsPlatform, object sender)
        {
            _sender = sender;
            EventsPlatform = eventsPlatform;
        }

        public EventsPlatform EventsPlatform { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Bind<T>(EventHandler<T> callback, EventPriority priority = EventPriority.Normal) where T : Event
        {
            if (Contains(callback))
            {
                throw new ArgumentException("Callback has already been added to the specified event.");
            }

            var binding = new Binding<T>(this, callback, priority);
            binding.Subscribe();
            _bindings.Add(binding);
        }

        public bool Contains<T>(EventHandler<T> callback) where T : Event
        {
            return
                _bindings.Any(binding => typeof (T) == binding.Type && binding.GetCallback() == (Delegate) callback);
        }

        public bool TryGetBinding<T>(EventHandler<T> callback, out IBinding binding) where T : Event
        {
            foreach (
                IBinding b in
                    _bindings.Where(
                        b => typeof (T) == b.Type && b.GetCallback() == (Delegate) callback))
            {
                binding = b;
                return true;
            }

            binding = null;
            return false;
        }

        public void Raise<T>(T eventArgs) where T : Event
        {
            EventsPlatform.Event<T>().Raise(_sender, eventArgs);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (IBinding binding in _bindings)
                {
                    binding.Unsubscribe();
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
                _parent = parent;
                _callback = callback;
                _priority = priority;
            }

            public Type Type
            {
                get { return typeof (T); }
            }

            public bool IsSubscribed
            {
                get { return _parent.EventsPlatform.Event<T>().Contains(_callback); }
            }

            public Delegate GetCallback()
            {
                return _callback;
            }

            public void Subscribe()
            {
                _parent.EventsPlatform.Event<T>().Bind(_callback, _priority);
            }

            public void Unsubscribe()
            {
                _parent.EventsPlatform.Event<T>().Remove(_callback);
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
    }
}