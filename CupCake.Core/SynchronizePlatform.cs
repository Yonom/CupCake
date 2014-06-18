using System;
using System.ComponentModel;
using System.Threading;
using MuffinFramework.Platforms;
using Nito.Async;

namespace CupCake.Core
{
    public class SynchronizePlatform : Platform
    {
        private ActionThread _thread;

        protected override void Enable()
        {
            this._thread = new ActionThread();
            this._thread.Start();
            this._thread.DoSynchronously(() => 
                this.SynchronizingObject = new GenericSynchronizingObject());
        }

        public ISynchronizeInvoke SynchronizingObject { get; private set; }

        public void Do(Action action)
        {
            this._thread.Do(action);
        }

        public bool DoSynchronously(Action action, TimeSpan timeout)
        {
            if (!SynchronizingObject.InvokeRequired)
                action();

            return this._thread.DoSynchronously(action, timeout);
        }

        public void DoSynchronously(Action action)
        {
            if (!SynchronizingObject.InvokeRequired)
                action();

            this._thread.DoSynchronously(action);
        }

        public T DoGet<T>(Func<T> action)
        {
            if (!SynchronizingObject.InvokeRequired)
                return action();

            return this._thread.DoGet(action);
        }
    }
}