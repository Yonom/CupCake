using System;
using CupCake.Core.Events;
using CupCake.Messages.Send;

namespace CupCake.Upload
{
    public class UploadRequestEvent : Event
    {
        private int _sendTries;
        private bool _verified;

        public UploadRequestEvent(IBlockPlaceSendEvent sendEvent)
        {
            SendEvent = sendEvent;
        }

        public IBlockPlaceSendEvent SendEvent { get; set; }

        /// <summary>
        ///     Determines whether the message should be put in the front or back of the queue.
        ///     Must be set before raising the Event.
        /// </summary>
        public bool IsUrgent { get; set; }

        /// <summary>
        ///     The number of times this message was attempted to be sent
        /// </summary>
        public int SendTries
        {
            get { return _sendTries; }
            internal set
            {
                _sendTries = value;
                OnTryingSend();
            }
        }

        /// <summary>
        ///     Gets whether this message has been successfully sent (after confirm)
        /// </summary>
        public bool Verified
        {
            get { return _verified; }
            internal set
            {
                _verified = value;
                if (value)
                    OnVerifyComplete();
            }
        }

        public event EventHandler TryingSend;

        protected virtual void OnTryingSend()
        {
            EventHandler handler = TryingSend;
            if (handler != null) handler(this, Empty);
        }

        public event EventHandler VerifyComplete;

        protected virtual void OnVerifyComplete()
        {
            EventHandler handler = VerifyComplete;
            if (handler != null) handler(this, Empty);
        }
    }
}