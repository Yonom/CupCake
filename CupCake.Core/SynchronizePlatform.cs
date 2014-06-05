using System;
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
        }


        public void Do(Action action)
        {
            this._thread.Do(action);
        }

        public bool DoSynchronously(Action action, TimeSpan timeout)
        {
            return this._thread.DoSynchronously(action, timeout);
        }

        public void DoSynchronously(Action action)
        {
            this._thread.DoSynchronously(action);
        }

        public T DoGet<T>(Func<T> action)
        {
            return this._thread.DoGet(action);
        }
    }
}