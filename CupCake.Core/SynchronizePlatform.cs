using System;
using System.ComponentModel;
using MuffinFramework.Platforms;
using Nito.Async;

namespace CupCake.Core
{
    public class SynchronizePlatform : Platform
    {
        private ActionThread _thread;

        public ISynchronizeInvoke SynchronizingObject { get; private set; }

        protected override void Enable()
        {
            _thread = new ActionThread {IsBackground = true};
            _thread.Start();
            _thread.DoSynchronously(() =>
                SynchronizingObject = new GenericSynchronizingObject());
        }

        public void Do(Action action)
        {
            _thread.Do(action);
        }

        public bool DoSynchronously(Action action, TimeSpan timeout)
        {
            if (!SynchronizingObject.InvokeRequired)
                action();

            return _thread.DoSynchronously(action, timeout);
        }

        public void DoSynchronously(Action action)
        {
            if (!SynchronizingObject.InvokeRequired)
                action();

            _thread.DoSynchronously(action);
        }

        public T DoGet<T>(Func<T> action)
        {
            if (!SynchronizingObject.InvokeRequired)
                return action();

            return _thread.DoGet(action);
        }
    }
}