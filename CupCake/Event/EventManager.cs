using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BotBits;
using BotBits.SendMessages;

namespace CupCake
{
    public class EventManager : IEnumerable<EventManager.IBinding>, IDisposable
    {
        private readonly List<IBinding> _bindings = new List<IBinding>();
        private readonly object _lockObj = new object();
        private readonly BotBitsClient _botBits;

        public EventManager(BotBitsClient botBits)
        {
            this._botBits = botBits;
        }

        void IDisposable.Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
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

        public void Bind<T>(EventRaiseHandler<T> callback, EventPriority priority = EventPriority.Normal) where T : Event<T>
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

        public bool Contains<T>(EventRaiseHandler<T> callback) where T : Event<T>
        {
            lock (this._lockObj)
            {
                return this._bindings.Any(
                    binding =>
                        typeof(T) == binding.Type &&
                        binding.GetCallback() == (Delegate)callback);
            }
        }

        public bool TryGetBinding<T>(EventRaiseHandler<T> callback, out IBinding binding) where T : Event<T>
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

        public void Raise<T>(T eventArgs) where T : Event<T>
        {
            eventArgs.RaiseIn(this._botBits);
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

        private class Binding<T> : IBinding where T : Event<T>
        {
            private readonly EventRaiseHandler<T> _callback;
            private readonly EventManager _parent;
            private readonly EventPriority _priority;

            public Binding(EventManager parent, EventRaiseHandler<T> callback, EventPriority priority)
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
                get { return Event<T>.Of(this._parent._botBits).Contains(this._callback); }
            }

            public Delegate GetCallback()
            {
                return this._callback;
            }

            public void Subscribe()
            {
                Event<T>.Of(this._parent._botBits).Bind(this._callback, this._priority);
            }

            public void Unsubscribe()
            {
                Event<T>.Of(this._parent._botBits).Unbind(this._callback);
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