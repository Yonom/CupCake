using System;
using CupCake.Core.Events;
using MuffinFramework.Platforms;

namespace CupCake.Core.Platforms
{
    public class EventsPlatform : Platform
    {
        private static int _lastId;

        private readonly int _id = ++_lastId;

        protected override void Enable()
        {
        }

        public EventHandle<T> Event<T>() where T : Event
        {
            return EventHandle<T>.Get(this._id);
        }
    }
}