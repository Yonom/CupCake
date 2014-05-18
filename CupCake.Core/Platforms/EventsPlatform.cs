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

        public EventManager<T> Event<T>() where T : EventArgs
        {
            return EventManager<T>.Get(this._id);
        }
    }
}