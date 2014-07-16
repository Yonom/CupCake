using System;
using System.Threading;

namespace CupCake.Upload
{
    internal sealed class DequeWorker
    {
        private readonly Deque<Action> _deque = new Deque<Action>();
        private readonly object _lockObj = new object();
        private readonly ManualResetEvent _resetEvent = new ManualResetEvent(false);

        private bool _stopping;
        private Thread _thread;

        public int Count
        {
            get
            {
                lock (_deque)
                {
                    return _deque.Count;
                }
            }
        }

        public bool IsAlive
        {
            get { return (_thread != null && _thread.IsAlive); }
        }

        public void Start()
        {
            lock (_lockObj)
            {
                if (!IsAlive)
                {
                    _stopping = false;

                    _thread = new Thread(Work)
                    {
                        IsBackground = true,
                        Name = "CupCake.Upload.DequeWorker"
                    };
                    _thread.Start();
                }
            }
        }

        public void Stop()
        {
            bool enabledStop = false;
            lock (_lockObj)
            {
                if (IsAlive)
                {
                    enabledStop = true;
                    _stopping = true;
                }
            }

            if (enabledStop)
            {
                _resetEvent.Set();
                _thread.Join();
            }
        }

        public void QueueFront(Action task)
        {
            lock (_deque)
            {
                _deque.AddToFront(task);

                if (_deque.Count == 1)
                {
                    _resetEvent.Set();
                }
            }
        }

        public void QueueBack(Action task)
        {
            lock (_deque)
            {
                _deque.AddToBack(task);

                if (_deque.Count == 1)
                {
                    _resetEvent.Set();
                }
            }
        }

        public void Clear()
        {
            lock (_deque)
            {
                _deque.Clear();
                _resetEvent.Reset();
            }
        }

        private void Work()
        {
            while (!_stopping)
            {
                // Wait for an action to arrive
                _resetEvent.WaitOne();

                if (_deque.Count > 0)
                {
                    Action task;
                    lock (_deque)
                    {
                        task = _deque.RemoveFromFront();

                        // Reset the signal if necessary
                        if (_deque.Count == 0)
                        {
                            _resetEvent.Reset();
                        }
                    }
                    task();
                }
            }
        }
    }
}