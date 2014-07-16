using System.Collections.Concurrent;
using CupCake.Core.Events;
using CupCake.Messages.Receive;

namespace CupCake.Messages
{
    public class MessageManager
    {
        private readonly EventManager _eventsPlatform;

        private readonly ConcurrentDictionary<string, IRegisteredMessage> _messageDictionary =
            new ConcurrentDictionary<string, IRegisteredMessage>();

        public MessageManager(EventManager eventsPlatform)
        {
            _eventsPlatform = eventsPlatform;
        }

        public bool TryGetMessage(string str, out IRegisteredMessage message)
        {
            return _messageDictionary.TryGetValue(str, out message);
        }

        public void RegisterMessage<T>(string str) where T : ReceiveEvent
        {
            _messageDictionary.TryAdd(str, new RegisteredMessage<T>(_eventsPlatform));
        }

        public bool UnRegisterMessage(string str)
        {
            IRegisteredMessage registeredMessage;
            return _messageDictionary.TryRemove(str, out registeredMessage);
        }

        public bool Contains(string str)
        {
            return _messageDictionary.ContainsKey(str);
        }

        public void UnRegisterAll()
        {
            _messageDictionary.Clear();
        }
    }
}