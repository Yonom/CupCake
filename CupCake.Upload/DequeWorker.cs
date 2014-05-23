using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Nito;

namespace CupCake.Upload
{
    internal sealed class DequeWorker
    {
        
        private readonly Thread _thread;
        private readonly ManualResetEvent _resetEvent = new ManualResetEvent(false);
        private readonly Deque<Action> _deque = new Deque<Action>();

        private bool _stopping;

        public DequeWorker()
        {
            this._thread = new Thread(this.Work)
            {
                IsBackground = true, 
                Name = "CupCake.Upload.DequeWorker"
            };
        }

        public int Count
        {
            get
            {
                lock (this._deque)
                {
                    return _deque.Count;
                }
            }
        }

        public void Start()
        {
            this._thread.Start();
        }

        public void Stop()
        {
            if (this._thread.IsAlive)
            {
                this._stopping = true;
                this._thread.Join();
            }
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

        private void Work()
        {
            while (!_stopping)
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
