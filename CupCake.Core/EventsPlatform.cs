using System;
using System.Collections.Concurrent;
using CupCake.Core.Events;
using MuffinFramework.Platforms;

namespace CupCake.Core
{
    public class EventsPlatform : Platform
    {
        private readonly ConcurrentDictionary<Type, object> _eventHandler = new ConcurrentDictionary<Type, object>();

        protected override void Enable()
        {
        }

        public EventHandle<T> Event<T>() where T : Event
        {
            return (EventHandle<T>)this._eventHandler.GetOrAdd(typeof(T), t => this.EnablePart<EventHandle<T>>());
        }
    }
}