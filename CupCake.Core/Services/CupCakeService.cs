using System;
using CupCake.Core.Events;
using CupCake.Core.Platforms;
using MuffinFramework.Services;

namespace CupCake.Core.Services
{
    public abstract class CupCakeService : CupCakeService<object>
    {
    }

    public abstract class CupCakeService<T> : Service<T>
    {
        private readonly Lazy<ConnectionPlatform> _connectionPlatform;

        private readonly Lazy<EventManager> _events;

        protected CupCakeService()
        {
            this._connectionPlatform = new Lazy<ConnectionPlatform>(() => this.PlatformLoader.Get<ConnectionPlatform>());

            this._events = new Lazy<EventManager>(() =>
            {
                var eventsPlatform = this.PlatformLoader.Get<EventsPlatform>();
                return new EventManager(eventsPlatform, this);
            });
        }

        public EventManager Events
        {
            get { return this._events.Value; }
        }

        public ConnectionPlatform ConnectionPlatform
        {
            get { return this._connectionPlatform.Value; }
        }

        protected override void Dispose(bool disposing)
        {
            this.Events.Dispose();

            base.Dispose(disposing);
        }
    }
}