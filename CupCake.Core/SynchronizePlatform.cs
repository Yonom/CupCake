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

        public ISynchronizeInvoke SynchronizingObject { get; private set; }

        protected override void Enable()
        {
            this._thread = new ActionThread {IsBackground = true};
            this._thread.Start();
            this._thread.DoSynchronously(() =>
                this.SynchronizingObject = new GenericSynchronizingObject());
        }

        public void Do(Action action)
        {
            this._thread.Do(action);
        }

        public bool DoSynchronously(Action action, TimeSpan timeout)
        {
            if (!this.SynchronizingObject.InvokeRequired)
                action();

            return this._thread.DoSynchronously(action, timeout);
        }

        public void DoSynchronously(Action action)
        {
            if (!this.SynchronizingObject.InvokeRequired)
                action();

            this._thread.DoSynchronously(action);
        }

        public T DoGet<T>(Func<T> action)
        {
            if (!this.SynchronizingObject.InvokeRequired)
                return action();

            return this._thread.DoGet(action);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThreadPool.QueueUserWorkItem(o =>
                    this._thread.Dispose());
            }

            base.Dispose(disposing);
        }
    }
}