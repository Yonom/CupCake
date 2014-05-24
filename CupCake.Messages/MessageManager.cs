using System.Collections.Concurrent;
using CupCake.Core.Events;
using CupCake.Messages.Events.Receive;

namespace CupCake.Messages
{
    public class MessageManager
    {
        private readonly EventManager _eventsPlatform;

        private readonly ConcurrentDictionary<string, IRegisteredMessage> _messageDictionary =
            new ConcurrentDictionary<string, IRegisteredMessage>();

        public MessageManager(EventManager eventsPlatform)
        {
            this._eventsPlatform = eventsPlatform;
        }

        public bool TryGetMessage(string str, out IRegisteredMessage message)
        {
            return this._messageDictionary.TryGetValue(str, out message);
        }

        public void RegisterMessage<T>(string str) where T : ReceiveEvent
        {
            this._messageDictionary.TryAdd(str, new RegisteredMessage<T>(this._eventsPlatform));
        }

        public bool UnRegisterMessage(string str)
        {
            IRegisteredMessage registeredMessage;
            return this._messageDictionary.TryRemove(str, out registeredMessage);
        }

        public bool Contains(string str)
        {
            return this._messageDictionary.ContainsKey(str);
        }

        public void UnRegisterAll()
        {
            this._messageDictionary.Clear();
        }
    }
}