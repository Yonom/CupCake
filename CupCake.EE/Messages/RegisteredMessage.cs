using System;
using CupCake.Core.Platforms;
using CupCake.EE.Messages.Receive;
using PlayerIOClient;

namespace CupCake.EE.Messages
{
    public class RegisteredMessage<T> : IRegisteredMessage where T : ReceiveMessage
    {
        private readonly EventsPlatform _eventsPlatform;

        public RegisteredMessage(EventsPlatform eventsPlatform)
        {
            this._eventsPlatform = eventsPlatform;
        }

        public void Invoke(object sender, Message message)
        {
            var instance = (T)Activator.CreateInstance(typeof(T), new object[] {message});

            this._eventsPlatform.Event<ReceiveMessage>().Raise(sender, instance);
            this._eventsPlatform.Event<T>().Raise(sender, instance);
        }
    }
}