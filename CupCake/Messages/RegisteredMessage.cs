using System;
using CupCake.Core.Events;
using CupCake.EE.Events.Receive;
using PlayerIOClient;

namespace CupCake.Messages
{
    public class RegisteredMessage<T> : IRegisteredMessage where T : ReceiveEvent
    {
        private readonly EventManager _events;

        public RegisteredMessage(EventManager events)
        {
            this._events = events;
        }

        public void Invoke(Message message)
        {
            var instance = (T)Activator.CreateInstance(typeof(T), message);

            this._events.Raise<ReceiveEvent>(instance);
            this._events.Raise(instance);
        }
    }
}