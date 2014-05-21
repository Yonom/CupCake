using System;
using CupCake.Core.Events;
using CupCake.Core.Platforms;
using MuffinFramework.Services;

namespace CupCake.Core.Services
{
    public abstract class CupCakeServicePart<TProtocol> : ServicePart<TProtocol>
    {
        private readonly Lazy<ConnectionPlatform> _connectionPlatform;

        private readonly Lazy<EventManager> _events;

        protected CupCakeServicePart()
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