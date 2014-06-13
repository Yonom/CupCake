using System;
using System.Threading;

namespace CupCake.Upload
{
    internal sealed class DequeWorker
    {
        private readonly Deque<Action> _deque = new Deque<Action>();
        private readonly object _lockObj = new object();
        private readonly ManualResetEvent _resetEvent = new ManualResetEvent(false);
        private Thread _thread;

        private bool _stopping;

        public int Count
        {
            get
            {
                lock (this._deque)
                {
                    return this._deque.Count;
                }
            }
        }

        public bool IsAlive
        {
            get { return this._thread.IsAlive; }
        }

        public void Start()
        {
            lock (this._lockObj)
            {
                if (!this._thread.IsAlive)
                {
                    this._stopping = false;

                    this._thread = new Thread(this.Work)
                    {
                        IsBackground = true,
                        Name = "CupCake.Upload.DequeWorker"
                    };
                    this._thread.Start();
                }
            }
        }

        public void Stop()
        {
            bool enabledStop = false;
            lock (this._lockObj)
            {
                if (this._thread.IsAlive)
                {
                    enabledStop = true;
                    this._stopping = true;
                }
            }

            if (enabledStop)
                this._thread.Join();
        }

        public void QueueFront(Action task)
        {
            lock (this._deque)
            {
                this._deque.AddToFront(task);

                if (this._deque.Count == 1)
                {
                    this._resetEvent.Set();
                }
            }
        }

        public void QueueBack(Action task)
        {
            lock (this._deque)
            {
                this._deque.AddToBack(task);

                if (this._deque.Count == 1)
                {
                    this._resetEvent.Set();
                }
            }
        }

        public void Clear()
        {
            lock (this._deque)
            {
                this._deque.Clear();
                this._resetEvent.Reset();
            }
        }

        private void Work()
        {
            while (!this._stopping)
            {
                // Wait for an action to arrive
                this._resetEvent.WaitOne();

                Action task;
                lock (this._deque)
                {
                    task = this._deque.RemoveFromFront();

                    // Reset the signal if necessary
                    if (this._deque.Count == 0)
                    {
                        this._resetEvent.Reset();
                    }
                }
                task();
            }
        }
    }
}