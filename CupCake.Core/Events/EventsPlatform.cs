using System;
using System.Collections.Concurrent;
using MuffinFramework.Platforms;

namespace CupCake.Core.Events
{
    public class EventsPlatform : Platform
    {
        private readonly ConcurrentDictionary<Type, object> _eventHandler = new ConcurrentDictionary<Type, object>();

        protected override void Enable()
        {
        }

        public EventHandle<T> Event<T>() where T : Event
        {
            return (EventHandle<T>) _eventHandler.GetOrAdd(typeof (T), t => EnablePart<EventHandle<T>>());
        }
    }
}